/* thermistor control MCU board IO
 * control command structure
 * data[0] = 0x04;
 * data[1] = 0xFF;
 * data[2] = 0x04; => control byte
 * data[3] = 0xF7;
 * data[4] = 0xFE;
 * A3 - high temperature simulation (low value resistor) / control byte value : 0x00
 * A4 - normal temperature simulation (normal value resistor) / control byte value : 0x02
 * A5 - low temperature simulation (high value resistor) / control byte value : 0x04
 * A6 - unassigned / control byte value : 0x06
 */


#define USE_UART
#define USE_AUTOTHMTEST

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using NationalInstruments.Visa;
using FTD2XX_NET; // wsjung.add.170517 : FT234XD gpio test
using System.IO;
using System.IO.Ports;

namespace AirohaChargingTest
{
    public partial class Form1 : Form
    {
        public MessageBasedSession mbSession;
        public string lastResourceString = null;

        string pathConfigFile;

        public string gVbatLowVoltage;
        public string gVbatCC2Voltage;
        public string gVbatHighVoltage;
        public string gVbatHighVoltageLimit; // wsjung.add.170805
        public string gVbatCurrentLimit;
        public string gChargerVoltage;
        public string gChargerCurrentLimit;
        public string gCurrentLvVbatLimitUpper;
        public string gCurrentLvVbatLimitLower;
        public string gCurrentLvChargerLimitUpper;
        public string gCurrentLvChargerLimitLower;
        public string gCurrentLvCC2VbatLimitUpper;
        public string gCurrentLvCC2VbatLimitLower;
        public string gCurrentLvCC2ChargerLimitUpper;
        public string gCurrentLvCC2ChargerLimitLower;
        public string gCurrentHvVbatLimitUpper;
        public string gCurrentHvVbatLimitLower;
        public string gCurrentHvChargerLimitUpper;
        public string gCurrentHvChargerLimitLower;
        public string gCurrentTHLimitLower;
        public string gCurrentTHLimitUpper;
        public string gCurrentTNLimitLower;
        public string gCurrentTNLimitUpper;
        public string gCurrentTLLimitLower;
        public string gCurrentTLLimitUpper;


        static public string gComPort;
        static public string gGpibAddress;

        static public bool gEnableVbusCalibration;

        public double gValCurrentLvVbatLimitUpper;
        public double gValCurrentLvVbatLimitLower;
        public double gValCurrentLvChargerLimitUpper;
        public double gValCurrentLvChargerLimitLower;
        public double gValCurrentLvCC2VbatLimitUpper;
        public double gValCurrentLvCC2VbatLimitLower;
        public double gValCurrentLvCC2ChargerLimitUpper;
        public double gValCurrentLvCC2ChargerLimitLower;
        public double gValCurrentHvVbatLimitUpper;
        public double gValCurrentHvVbatLimitLower;
        public double gValCurrentHvChargerLimitUpper;
        public double gValCurrentHvChargerLimitLower;
        public double gValCurrentTHLimitLower;
        public double gValCurrentTHLimitUpper;
        public double gValCurrentTNLimitLower;
        public double gValCurrentTNLimitUpper;
        public double gValCurrentTLLimitLower;
        public double gValCurrentTLLimitUpper;



        public int gCalStep = 0;

        // measured value
        public double gMeasValCcVbat = 0.000;
        public double gMeasValCcVbus = 0.000;
        public double gMeasValCvVbat = 0.000;
        public double gMeasValCvVbus = 0.000;
        public double gMeasValCvCC2Vbat = 0.000;
        public double gMeasValCvCC2Vbus = 0.000;
        public double gMeasResultThmHigh = 0.000;
        public double gMeasResultThmNormal = 0.000;
        public double gMeasResultThmLow = 0.000;

        public string gCalValue = "";

        public string gModelName = ""; // wsjung.add.170808


        public string pathLogFile;

        //public double 

        public string readString = "";

        public Form1()
        {
            InitializeComponent();
        }

#if (USE_UART)
        private SerialPort _Port;
        private SerialPort Port
        {
            get
            {
                if (_Port == null)
                {
                    _Port = new SerialPort();
                    _Port.PortName = "COM47";
                    _Port.BaudRate = 115200;
                    _Port.DataBits = 8;
                    _Port.Parity = Parity.None;
                    _Port.Handshake = Handshake.None;
                    _Port.StopBits = StopBits.One;
                    _Port.DataReceived += Port_DataReceived;
                }
                return _Port;
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(200);

            byte[] bArray = new byte[50];

            int xbb = Port.BytesToRead;
            //MessageBox.Show(xbb.ToString());

            string msg = "";
            for (int i = 0; i < xbb; i++)
            {
                bArray[i] = Convert.ToByte(Port.ReadByte());
                msg += String.Format("0x{0:X2} ", bArray[i]);
            }
        }

        private Boolean IsOpen
        {
            get { return Port.IsOpen; }
            set
            {
                if (value)
                {
                    //
                }
                else
                {
                    //
                }
            }
        }

        private bool openPort()
        {
            bool flag_ng = false;

            if (!Port.IsOpen)
            {
                Port.PortName = gComPort;

                try
                {
                    // 연결
                    Port.Open();
                }
                catch // (Exception ex)
                {
                    flag_ng = true;
                }
            }
            else
            {
                // 현재 시리얼이 연결 상태이면 연결 해제
                Port.Close();
            }

            // 상태 변경
            IsOpen = Port.IsOpen;

            if (flag_ng) { return false; }
            else { return true; }
        }

        private void sendByteMsg(byte[] data)
        {
            // 보낼 메시지가 없으면 종료
            String text = gComPort;
            if (String.IsNullOrEmpty(text)) return;

            try
            {
                // 메시지 전송
                //Port.WriteLine(text);
                Port.Write(data, 0, data.Length);

                string sendData = "";
                for (int i = 0; i < data.Length; i++)
                {
                    sendData += String.Format("0x{0:X2} ", data[i]);
                }
            }
            catch //(Exception ex)
            {
                //
            }
        }
#endif // (USE_UART)

        private void Form1_Load(object sender, EventArgs e)
        {
            // load config
            loadConfig();

#if (USE_UART)
            if (!openPort())
            {
                MessageBox.Show("Check UART connection!!! - NG");
                Close();
            }
#endif // #if (USE_UART)

            checkLogFile(); // 170306


            this.Text = gModelName + " Charging Test " + " v0.0.0.1 " + " for ES2 Event ";

            // check battery simulator
            if (checkSimulator())
            {
                // set some display(limit)
                lb_lv_current_vbat_lower.Text = gCurrentLvVbatLimitLower;
                lb_lv_current_vbat_upper.Text = gCurrentLvVbatLimitUpper;
                lb_lv_current_vbus_lower.Text = gCurrentLvChargerLimitLower;
                lb_lv_current_vbus_upper.Text = gCurrentLvChargerLimitUpper;

                lb_lv_cc2_current_vbat_lower.Text = gCurrentLvCC2VbatLimitLower;
                lb_lv_cc2_current_vbat_upper.Text = gCurrentLvCC2VbatLimitUpper;
                lb_lv_cc2_current_vbus_lower.Text = gCurrentLvCC2ChargerLimitLower;
                lb_lv_cc2_current_vbus_upper.Text = gCurrentLvCC2ChargerLimitUpper;

                lb_hv_current_vbat_lower.Text = gCurrentHvVbatLimitLower;
                lb_hv_current_vbat_upper.Text = gCurrentHvVbatLimitUpper;
                lb_hv_current_vbus_lower.Text = gCurrentHvChargerLimitLower;
                lb_hv_current_vbus_upper.Text = gCurrentHvChargerLimitUpper;


                // convert limit text to double value
                gValCurrentLvVbatLimitLower = Convert.ToDouble(gCurrentLvVbatLimitLower);
                gValCurrentLvVbatLimitUpper = Convert.ToDouble(gCurrentLvVbatLimitUpper);

                gValCurrentLvChargerLimitLower = Convert.ToDouble(gCurrentLvChargerLimitLower);
                gValCurrentLvChargerLimitUpper = Convert.ToDouble(gCurrentLvChargerLimitUpper);

                gValCurrentLvCC2VbatLimitLower = Convert.ToDouble(gCurrentLvCC2VbatLimitLower);
                gValCurrentLvCC2VbatLimitUpper = Convert.ToDouble(gCurrentLvCC2VbatLimitUpper);

                gValCurrentLvCC2ChargerLimitLower = Convert.ToDouble(gCurrentLvCC2ChargerLimitLower);
                gValCurrentLvCC2ChargerLimitUpper = Convert.ToDouble(gCurrentLvCC2ChargerLimitUpper);
                
                gValCurrentHvVbatLimitLower = Convert.ToDouble(gCurrentHvVbatLimitLower);
                gValCurrentHvVbatLimitUpper = Convert.ToDouble(gCurrentHvVbatLimitUpper);

                gValCurrentHvChargerLimitLower = Convert.ToDouble(gCurrentHvChargerLimitLower);
                gValCurrentHvChargerLimitUpper = Convert.ToDouble(gCurrentHvChargerLimitUpper);


                Thread.Sleep(500);

                // set basic parameter
                gpib_write("*rst");
                Thread.Sleep(200);

                gpib_write("DISP:CHAN 1");
                gpib_write("VOLT 3.700");
                gpib_write("CURR 550e-3");
                gpib_write("outp off");
                gpib_write("DISP:CHAN 2");
                gpib_write("SOUR2:VOLT 5.000");
                gpib_write("SOUR2:CURR 550e-3");
                gpib_write("outp2 off");

            }
            else
            {
                //btnStart.Enabled = false;
                Close();
            }
        }

        private void loadConfig()
        {
            // using ini style config
            pathConfigFile = System.IO.Directory.GetCurrentDirectory() + "\\" + "config\\" + "config.ini";
            string tempString;

            IniReadWrite IniReader = new IniReadWrite();

            tempString = IniReader.IniReadValue("CONFIG", "VBAT_LOW_VOLTAGE", pathConfigFile);
            gVbatLowVoltage = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "VBAT_CC2_VOLTAGE", pathConfigFile);
            gVbatCC2Voltage = tempString;
            
            tempString = IniReader.IniReadValue("CONFIG", "VBAT_HIGH_VOLTAGE", pathConfigFile);
            gVbatHighVoltage = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "VBAT_HIGH_VOLTAGE_LIMIT", pathConfigFile); // wsjung.add.170805
            gVbatHighVoltageLimit = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "VBAT_CURRENT_LIMIT", pathConfigFile);
            gVbatCurrentLimit = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CHARGER_VOLTAGE", pathConfigFile);
            gChargerVoltage = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CHARGER_CURRENT_LIMIT", pathConfigFile);
            gChargerCurrentLimit = tempString;


            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_VBAT_LIMIT_UPPER", pathConfigFile);
            gCurrentLvVbatLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_VBAT_LIMIT_LOWER", pathConfigFile);
            gCurrentLvVbatLimitLower = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_CHARGER_LIMIT_UPPER", pathConfigFile);
            gCurrentLvChargerLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_CHARGER_LIMIT_LOWER", pathConfigFile);
            gCurrentLvChargerLimitLower = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_CC2_VBAT_LIMIT_UPPER", pathConfigFile);
            gCurrentLvCC2VbatLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_CC2_VBAT_LIMIT_LOWER", pathConfigFile);
            gCurrentLvCC2VbatLimitLower = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_CC2_CHARGER_LIMIT_UPPER", pathConfigFile);
            gCurrentLvCC2ChargerLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_LV_CC2_CHARGER_LIMIT_LOWER", pathConfigFile);
            gCurrentLvCC2ChargerLimitLower = tempString;


            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_HV_VBAT_LIMIT_UPPER", pathConfigFile);
            gCurrentHvVbatLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_HV_VBAT_LIMIT_LOWER", pathConfigFile);
            gCurrentHvVbatLimitLower = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_HV_CHARGER_LIMIT_UPPER", pathConfigFile);
            gCurrentHvChargerLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_HV_CHARGER_LIMIT_LOWER", pathConfigFile);
            gCurrentHvChargerLimitLower = tempString;


            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_TH_LIMIT_LOWER", pathConfigFile);
            gCurrentTHLimitLower = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_TH_LIMIT_UPPER", pathConfigFile);
            gCurrentTHLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_TN_LIMIT_LOWER", pathConfigFile);
            gCurrentTNLimitLower = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_TN_LIMIT_UPPER", pathConfigFile);
            gCurrentTNLimitUpper = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_TL_LIMIT_LOWER", pathConfigFile);
            gCurrentTLLimitLower = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CURRENT_TL_LIMIT_UPPER", pathConfigFile);
            gCurrentTLLimitUpper = tempString;


            tempString = IniReader.IniReadValue("CONFIG", "PORT", pathConfigFile);
            gComPort = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "GPIB_ADDRESS", pathConfigFile);
            gGpibAddress = tempString;

            tempString = IniReader.IniReadValue("CONFIG", "CAL_STEP", pathConfigFile);
            gCalStep = Convert.ToInt32(tempString);

            tempString = IniReader.IniReadValue("CONFIG", "ENABLE_VBUS_CURRENT_CAL", pathConfigFile);
            if (tempString == "YES") { gEnableVbusCalibration = true; }
            else if (tempString == "NO") { gEnableVbusCalibration = false; }

            tempString = IniReader.IniReadValue("CONFIG", "MODEL_NAME", pathConfigFile); // wsjung.add.170808
            gModelName = tempString;
        }

        private void clearDisp()
        {
            tb_HvCvCurrentAtVbat.Text = "--";
            tb_HvCvCurrentAtVbus.Text = "--";
            tb_LvCcCurrentAtVbat.Text = "--";
            tb_LvCcCurrentAtVbus.Text = "--";
            tb_LvCC2CcCurrentAtVbat.Text = "--";
            tb_LvCC2CcCurrentAtVbus.Text = "--";

            tb_HvCvCurrentAtVbat.BackColor = Color.Empty;
            tb_HvCvCurrentAtVbus.BackColor = Color.Empty;
            tb_LvCcCurrentAtVbat.BackColor = Color.Empty;
            tb_LvCcCurrentAtVbus.BackColor = Color.Empty;
            tb_LvCC2CcCurrentAtVbat.BackColor = Color.Empty;
            tb_LvCC2CcCurrentAtVbus.BackColor = Color.Empty;

            lbResult.Text = "--";
            lbResult.ForeColor = Color.Empty;


            // 170306
            gMeasValCcVbat = 0.000;
            gMeasValCcVbus = 0.000;
            gMeasValCvVbat = 0.000;
            gMeasValCvVbus = 0.000;
            gMeasValCvCC2Vbat = 0.000;
            gMeasValCvCC2Vbus = 0.000;
            gMeasResultThmHigh = 0.000;
            gMeasResultThmNormal = 0.000;
            gMeasResultThmLow = 0.000;

            gCalValue = "";
        }


        private void setResistorDefault()
        {
            thmMcuA4On();
        }

        private bool checkSimulator()
        {
            {
                int flag_ng = 0;

                //lastResourceString = sr.ResourceName;
                //Cursor.Current = Cursors.WaitCursor;

                using (var rmSession = new ResourceManager())
                {
                    //string abcd = "GPIB0::11::INSTR";
                    string abcd = "GPIB0::" + gGpibAddress + "::INSTR";
                    //MessageBox.Show(abcd);

                    try
                    {
                        mbSession = (MessageBasedSession)rmSession.Open(abcd);
                    }
                    catch (InvalidCastException)
                    {
                        flag_ng = 1;
                        MessageBox.Show("Resource selected must be a message-based session");
                    }
                    catch //(Exception exp)
                    {
                        flag_ng = 1;
                        //MessageBox.Show(exp.Message);
                    }
                    finally
                    {
                        //...
                    }

                    // read instrument information
                    if (flag_ng != 1)
                    {
                        readString = "";
                        gpib_query("*IDN?");
                        if (readString == "")
                        {
                            flag_ng = 1;
                        }
                    }

                    if (flag_ng == 1)
                    {
                        MessageBox.Show("Battery simulator was not found - NG");
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Battery simulator was found - OK");
                        return true;
                    }
                }
            }
        }

        private bool gpib_query(string arg)
        {
            int flag_ng = 0;
            //string readValue = "";

            try
            {
                mbSession.RawIO.Write(arg);
                readString = InsertCommonEscapeSequences(mbSession.RawIO.ReadString());
            }
            catch (Exception exp)
            {
                flag_ng = 1;
                MessageBox.Show(exp.Message);
            }
            finally
            {
                //Cursor.Current = Cursors.Default;
            }

            if (flag_ng == 1)
            {
                return false;
            }
            else
            {
                //MessageBox.Show(readString);
                return true;
            }
        }

        private bool gpib_write(string arg)
        {
            int flag_ng = 0;

            try
            {
                mbSession.RawIO.Write(arg);
            }
            catch (Exception exp)
            {
                flag_ng = 1;
                MessageBox.Show(exp.Message);
            }

            if (flag_ng == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }


        #region simulator_control_functions
        public void setSimulatorCh1Off()
        {
            gpib_write("DISP:CHAN 1");
            gpib_write("outp off");
        }

        public void setSimulatorCh1On()
        {
            gpib_write("DISP:CHAN 1");
            gpib_write("outp on");
        }

        public void setSimulatorCh2Off()
        {
            gpib_write("DISP:CHAN 2");
            gpib_write("outp2 off");
        }

        public void setSimulatorCh2On()
        {
            gpib_write("DISP:CHAN 2");
            gpib_write("outp2 on");
        }

        public void setSimulatorCh1Voltage(string ch1Voltage)
        {
            string temp = "VOLT " + ch1Voltage;
            gpib_write("DISP:CHAN 1");
            gpib_write(temp);
        }

        public void setSimulatorCh2Voltage(string ch2Voltage)
        {
            string temp = "SOUR2:VOLT " + ch2Voltage;
            gpib_write("DISP:CHAN 2");
            gpib_write(temp);
        }

        public void setSimulatorCh1Current(string ch1CurrentLimit)
        {
            string temp = "CURR " + ch1CurrentLimit;
            gpib_write("DISP:CHAN 1");
            gpib_write(temp);
        }

        public void setSimulatorCh2Current(string ch2CurrentLimit)
        {
            string temp = "SOUR2:CURR " + ch2CurrentLimit;
            gpib_write("DISP:CHAN 2");
            gpib_write(temp);
        }

        public double getSimulatorCh1Current()
        {
            double sum = 0.000;

            gpib_write("DISP:CHAN 1");
            gpib_write("SENS:FUNC 'CURR'");
            Thread.Sleep(1500);

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1500);

                //gpib_write("SENS:FUNC 'CURR'");
                gpib_query("READ?");

                readString = readString.Replace("\\n", string.Empty);
                sum += double.Parse(readString);
            }

            return (double)(sum / 3);
        }

        public double getSimulatorCh2Current()
        {
            double sum = 0.000;

            gpib_write("DISP:CHAN 2");
            gpib_write("SENS2:FUNC 'CURR'");
            Thread.Sleep(1500);

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1500);

                //gpib_write("SENS2:FUNC 'CURR'");
                gpib_query("READ2?");

                readString = readString.Replace("\\n", string.Empty);
                sum += double.Parse(readString);
            }

            return (double)(sum / 3);
        }

        public void setSimulatorCh1ToReadCurr()
        {
            gpib_write("DISP:CHAN 1");
            gpib_write("SENS:FUNC 'CURR'");
            Thread.Sleep(500);
        }

        public double getSimulatorCh1CurrentOneTime()
        {
            double sum = 0.000;

            gpib_query("READ?");

            readString = readString.Replace("\\n", string.Empty);
            sum += double.Parse(readString);

            return sum;
        }
        #endregion

        #region delegates for update UI
        delegate void updateStatusDelegate(string str);


        delegate void updateThmStatusDelegate(double measValue, int sel, bool result);
        public void updateThmStatus(double measValue, int sel, bool result)
        {
            if (InvokeRequired)
            {
                updateThmStatusDelegate showThmStatusDel = new updateThmStatusDelegate(updateThmStatus);
                Invoke(showThmStatusDel, measValue, sel, result);
            }
            else
            {
                if (result == true)
                {
                    writeLog(gCalValue, gMeasValCcVbat, gMeasValCcVbus, gMeasValCvCC2Vbat, gMeasValCvCC2Vbus, gMeasValCvVbat, gMeasValCvVbus, "PASS"); 
                    lbResult.Text = "PASS";
                    lbResult.ForeColor = Color.Blue;
                    btnStart.Enabled = true;
                }
                else
                {
                    writeLog(gCalValue, gMeasValCcVbat, gMeasValCcVbus, gMeasValCvCC2Vbat, gMeasValCvCC2Vbus, gMeasValCvVbat, gMeasValCvVbus, "FAIL"); 
                    lbResult.Text = "FAIL";
                    lbResult.ForeColor = Color.Red;
                    btnStart.Enabled = true;
                }
            }
        }

        delegate void showCcCurrentResultDelegate(double measValue, int type, bool result);
        public void showCcCurrentResult(double measValue, int type, bool result)
        {
            if (InvokeRequired)
            {
                showCcCurrentResultDelegate resultCcCurrentDel = new showCcCurrentResultDelegate(showCcCurrentResult);
                Invoke(resultCcCurrentDel, measValue, type, result);
            }
            else
            {
                switch (type)
                {
                    case 0: // Low voltage, Vbat Current
                        //tb_LvCcCurrentAtVbat.Text = Convert.ToString(measValue);
                        tb_LvCcCurrentAtVbat.Text = measValue.ToString("F4");
                        if (result == false) { tb_LvCcCurrentAtVbat.BackColor = Color.Red; }
                        else { tb_LvCcCurrentAtVbat.BackColor = Color.SkyBlue; }
                        break;
                    case 1: // Low voltage, Vbus Current
                            //tb_LvCcCurrentAtVbus.Text = Convert.ToString(measValue);
                        tb_LvCcCurrentAtVbus.Text = measValue.ToString("F4");
                        if (result == false) { tb_LvCcCurrentAtVbus.BackColor = Color.Red; }
                        else { tb_LvCcCurrentAtVbus.BackColor = Color.SkyBlue; }
                        break;
                    case 2: // High voltage, Vbat Current
                            //tb_HvCvCurrentAtVbat.Text = Convert.ToString(measValue);
                        tb_HvCvCurrentAtVbat.Text = measValue.ToString("F4");
                        if (result == false) { tb_HvCvCurrentAtVbat.BackColor = Color.Red; }
                        else { tb_HvCvCurrentAtVbat.BackColor = Color.SkyBlue; }
                        break;
                    case 3: // High voltage, Vbus Current
                        //tb_HvCvCurrentAtVbus.Text = Convert.ToString(measValue);
                        tb_HvCvCurrentAtVbus.Text = measValue.ToString("F4");
                        if (result == false) { tb_HvCvCurrentAtVbus.BackColor = Color.Red; }
                        else { tb_HvCvCurrentAtVbus.BackColor = Color.SkyBlue; }
                        break;
                    case 4: // CC2 voltage, Vbat Current
                        //tb_LvCcCurrentAtVbat.Text = Convert.ToString(measValue);
                        tb_LvCC2CcCurrentAtVbat.Text = measValue.ToString("F4");
                        if (result == false) { tb_LvCC2CcCurrentAtVbat.BackColor = Color.Red; }
                        else { tb_LvCC2CcCurrentAtVbat.BackColor = Color.SkyBlue; }
                        break;
                    case 5: // CC2 voltage, Vbus Current
                            //tb_LvCcCurrentAtVbus.Text = Convert.ToString(measValue);
                        tb_LvCC2CcCurrentAtVbus.Text = measValue.ToString("F4");
                        if (result == false) { tb_LvCC2CcCurrentAtVbus.BackColor = Color.Red; }
                        else { tb_LvCC2CcCurrentAtVbus.BackColor = Color.SkyBlue; }
                        break;
                }

                //tb_LvCcCurrentAtVbat.Text = Convert.ToString(measValue);
            }
        }

        delegate void toggleBtnDelegate(Boolean x);
        public void toggleBtn(Boolean x)
        {
            if (InvokeRequired)
            {
                toggleBtnDelegate btnDel = new toggleBtnDelegate(toggleBtn);
                Invoke(btnDel, x);
            }
            else
            {
                if (x == true) { btnStart.Enabled = true; btnStart.Focus(); }
                else { btnStart.Enabled = false;}
            }
        }
        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            clearDisp();
            btnStart.Enabled = false;
            //thmMcuAllOff();
            
            thmMcuA4On();

            
            backWork bg = new backWork(this);
            Thread workerThread = new Thread(bg.DoWork2);
            workerThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mbSession != null)
            {
                mbSession.Dispose();
            }
        }

        private bool checkLogFile()
        {
            DateTime today = DateTime.Now;
            //string strDatePrefix = string.Format("{0:YY-MM-DD}", today);
            string strDatePrefix = today.ToString("yy-MM-dd");
            string strLogFile = strDatePrefix + "_" + gModelName + "_Charging" + "-log.csv";

            // set file path
            pathLogFile = System.IO.Directory.GetCurrentDirectory() + "\\" + "log\\" + strLogFile;

            if (File.Exists(pathLogFile))
            {
                // re-set test Cound
                StreamReader sr = new StreamReader(pathLogFile);
                string line;
                UInt32 lineCount = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    lineCount++;
                }
                sr.Close();

                return true;
            }
            else
            {
                //make new log file
                StreamWriter sw = new StreamWriter(pathLogFile, true, Encoding.Unicode);

                // write basic data (index)
                sw.WriteLine("date\t" + "cc vbat\t" + "cc vbus\t" + "cc2 vbat\t" + "cc2 vbus\t" + "cv vbat\t" + "cv vbus\t" + "result");
                sw.Close();

                return false;
            }
        }

        private void writeLog(string day, double ccVbat, double ccVbus, double cc2Vbat, double cc2Vbus, double cvVbat, double cvVbus, string result)
        {
            string date = DateTime.Now.ToString("yy-MM-dd");

            string strCcVbat = ccVbat.ToString("F4");
            string strCcVbus = ccVbus.ToString("F4");
            string strCc2Vbat = cc2Vbat.ToString("F4");
            string strCc2Vbus = cc2Vbus.ToString("F4");
            string strCvVbat = cvVbat.ToString("F4");
            string strCvVbus = cvVbus.ToString("F4");

            // open stream
            StreamWriter sw = new StreamWriter(pathLogFile, true, Encoding.Unicode);

            // write measured data
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", date, strCcVbat, strCcVbus, strCc2Vbat, strCc2Vbus, strCvVbat, strCvVbus, result);
            // close stream
            sw.Close();
        }

        public void thmMcuA3On() // high temperature simulation (low value resistor)
        {
            byte[] data = new byte[5];

            data[0] = 0x04;
            data[1] = 0xFF;
            data[2] = 0x00;
            data[3] = 0xF7;
            data[4] = 0xFE;

            sendByteMsg(data);
        }

        public void thmMcuA4On() // normal temperature simulation (normal value resistor)
        {
            byte[] data = new byte[5];

            data[0] = 0x04;
            data[1] = 0xFF;
            data[2] = 0x02;
            data[3] = 0xF7;
            data[4] = 0xFE;

            sendByteMsg(data);
        }

        public void thmMcuA5On() // low temperature simulation (high value resistor)
        {
            byte[] data = new byte[5];

            data[0] = 0x04;
            data[1] = 0xFF;
            data[2] = 0x04;
            data[3] = 0xF7;
            data[4] = 0xFE;

            sendByteMsg(data);
        }

        public void thmMcuA6On() // unsssigned
        {
            byte[] data = new byte[5];

            data[0] = 0x04;
            data[1] = 0xFF;
            data[2] = 0x06;
            data[3] = 0xF7;
            data[4] = 0xFE;

            sendByteMsg(data);
        }

        public void thmMcuAllOff()
        {
            byte[] data = new byte[5];

            data[0] = 0x04;
            data[1] = 0xFF;
            data[2] = 0x01;
            data[3] = 0xF7;
            data[4] = 0xFE;

            sendByteMsg(data);
        }
    }


    class backWork
    {
        Form1 mainForm;

        Airoha.Bluetooth.Protocol.HCI.IHost host = null;
        Airoha.Bluetooth.Device.IDevice dev = null;

        public backWork(Form1 frm)
        {
            mainForm = frm;
        }

        public void DoWork2()
        {

            mainForm.toggleBtn(false);
            
            Thread.Sleep(500);

            int flag_ng = 0;

            //string tempString = "";

            double measLvCcVbatCurrent = 0.000;
            double measLvCcVbusCurrent = 0.000;
            double measLvCC2CcVbatCurrent = 0.000;
            double measLvCC2CcVbusCurrent = 0.000;
            double measHvCvVbatCurrent = 0.000;
            double measHvCvVbusCurrent = 0.000;

            mainForm.setSimulatorCh1Voltage(mainForm.gVbatLowVoltage);
            //mainForm.setSimulatorCh1Current(mainForm.gVbatCurrentLimit);
            Thread.Sleep(500);
            mainForm.setSimulatorCh1On();
            Thread.Sleep(500); // 3000.ath-s220bt.kisun.20201215
            mainForm.setSimulatorCh2On();
            Thread.Sleep(500); // 2000.ath-s220bt.kisun.20201215


            // 01. get vbus current in low voltage
            if (flag_ng != 1)
            {
                measLvCcVbusCurrent = mainForm.getSimulatorCh2Current();

                if (((measLvCcVbusCurrent < mainForm.gValCurrentLvChargerLimitUpper) && (measLvCcVbusCurrent > mainForm.gValCurrentLvChargerLimitLower)))
                {
                    flag_ng = 0;

                    // update UI
                    mainForm.gMeasValCcVbus = measLvCcVbusCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCcVbusCurrent, 1, true);
                }
                else
                {
                    flag_ng = 1;

                    // update UI
                    mainForm.gMeasValCcVbus = measLvCcVbusCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCcVbusCurrent, 1, false);
                }
            }
            

            // 02. get vbat current in low voltage
            if (flag_ng != 1)
            {
                measLvCcVbatCurrent = mainForm.getSimulatorCh1Current();

                if ((measLvCcVbatCurrent < mainForm.gValCurrentLvVbatLimitUpper) && (measLvCcVbatCurrent > mainForm.gValCurrentLvVbatLimitLower))
                {
                    flag_ng = 0;

                    // update UI
                    mainForm.gMeasValCcVbat = measLvCcVbatCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCcVbatCurrent, 0, true);
                }
                else
                {
                    flag_ng = 1;

                    // update UI
                    mainForm.gMeasValCcVbat = measLvCcVbatCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCcVbatCurrent, 0, false);
                }
            }          

            // 03. change battery voltage to CC2
            if (flag_ng != 1)
            {
                mainForm.setSimulatorCh2Off();
                Thread.Sleep(200);
                //mainForm.setSimulatorCh1Off();
                //Thread.Sleep(200);

                mainForm.setSimulatorCh1Voltage(mainForm.gVbatCC2Voltage);

                mainForm.setSimulatorCh2On();
                Thread.Sleep(200);// 3000.ath-s220bt.kisun.20201215
                //mainForm.setSimulatorCh1On();
                //Thread.Sleep(1000);

                mainForm.setSimulatorCh2Off();
                Thread.Sleep(200);
                //mainForm.setSimulatorCh1Off();
                //Thread.Sleep(200);    

                // turn on again
                //mainForm.setSimulatorCh1On();
                //Thread.Sleep(200);
                mainForm.setSimulatorCh2On();
                Thread.Sleep(200); // 3000.ath-s220bt.kisun.20201215
            }

            // 04. get vbus current in CC2 voltage
            if (flag_ng != 1)
            {
                measLvCC2CcVbusCurrent = mainForm.getSimulatorCh2Current();

                if (((measLvCC2CcVbusCurrent < mainForm.gValCurrentLvCC2ChargerLimitUpper) && (measLvCC2CcVbusCurrent > mainForm.gValCurrentLvCC2ChargerLimitLower)))
                {
                    flag_ng = 0;

                    // update UI
                    mainForm.gMeasValCvCC2Vbus = measLvCC2CcVbusCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCC2CcVbusCurrent, 5, true);
                }
                else
                {
                    flag_ng = 1;

                    // update UI
                    mainForm.gMeasValCvCC2Vbus = measLvCC2CcVbusCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCC2CcVbusCurrent, 5, false);
                }
            }


            // 05. get vbat current in CC2 voltage
            if (flag_ng != 1)
            {
                measLvCC2CcVbatCurrent = mainForm.getSimulatorCh1Current();

                if ((measLvCC2CcVbatCurrent < mainForm.gValCurrentLvCC2VbatLimitUpper) && (measLvCC2CcVbatCurrent > mainForm.gValCurrentLvCC2VbatLimitLower))
                {
                    flag_ng = 0;

                    // update UI
                    mainForm.gMeasValCvCC2Vbat = measLvCC2CcVbatCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCC2CcVbatCurrent, 4, true);
                }
                else
                {
                    flag_ng = 1;

                    // update UI
                    mainForm.gMeasValCvCC2Vbat = measLvCC2CcVbatCurrent; // 170306
                    mainForm.showCcCurrentResult(measLvCC2CcVbatCurrent, 4, false);
                }
            }

            // 06. change battery voltage to high
            if (flag_ng != 1)
            {
                mainForm.setSimulatorCh2Off();
                Thread.Sleep(200);
                //mainForm.setSimulatorCh1Off();
                //Thread.Sleep(200);

                mainForm.setSimulatorCh1Voltage(mainForm.gVbatHighVoltage);

                mainForm.setSimulatorCh2On();
                Thread.Sleep(200);// 3000.ath-s220bt.kisun.20201215
                //mainForm.setSimulatorCh1On();
                //Thread.Sleep(1000);

                mainForm.setSimulatorCh2Off();
                Thread.Sleep(200);
                //mainForm.setSimulatorCh1Off();
                //Thread.Sleep(200);    

                // turn on again
                //mainForm.setSimulatorCh1On();
                //Thread.Sleep(200);
                mainForm.setSimulatorCh2On();
                Thread.Sleep(200); // 3000.ath-s220bt.kisun.20201215
            }

            // 07. get vbus current in high voltage
            if (flag_ng != 1)
            {
                measHvCvVbusCurrent = mainForm.getSimulatorCh2Current();

                if ((measHvCvVbusCurrent < mainForm.gValCurrentHvChargerLimitUpper) && (measHvCvVbusCurrent > mainForm.gValCurrentHvChargerLimitLower))
                {
                    flag_ng = 0;

                    // update UI
                    mainForm.gMeasValCvVbus = measHvCvVbusCurrent; // 170306
                    mainForm.showCcCurrentResult(measHvCvVbusCurrent, 3, true);
                }
                else
                {
                    // wsjung.add.170805 : try adjust full vbat battery (more high) until reach vbat high limit (ex: 4.25V)
                    flag_ng = 1;

                    double vbatStart = Convert.ToDouble(mainForm.gVbatHighVoltage) + 0.01;
                    double vbatLimit = Convert.ToDouble(mainForm.gVbatHighVoltageLimit);

                    while (vbatStart <= vbatLimit)
                    {
                        mainForm.setSimulatorCh1Voltage(Convert.ToString(vbatStart)); // change vbat
                        Thread.Sleep(500);

                        measHvCvVbusCurrent = mainForm.getSimulatorCh2Current();

                        if ((measHvCvVbusCurrent < mainForm.gValCurrentHvChargerLimitUpper) && (measHvCvVbusCurrent > mainForm.gValCurrentHvChargerLimitLower))
                        {
                            flag_ng = 0;
                            break;
                        }

                        vbatStart = vbatStart + 0.01; // increase
                    }

                    if (flag_ng == 0)
                    {
                        // update UI
                        mainForm.gMeasValCvVbus = measHvCvVbusCurrent; // 170306
                        mainForm.showCcCurrentResult(measHvCvVbusCurrent, 3, true);
                    }
                    else
                    {
                        // update UI
                        mainForm.gMeasValCvVbus = measHvCvVbusCurrent; // 170306
                        mainForm.showCcCurrentResult(measHvCvVbusCurrent, 3, false);
                    }

                }
            }

            // 08. get vbat current in high voltage
            if (flag_ng != 1)
            {
                measHvCvVbatCurrent = mainForm.getSimulatorCh1Current();

                if ((measHvCvVbatCurrent < mainForm.gValCurrentHvVbatLimitUpper) && (measHvCvVbatCurrent > mainForm.gValCurrentHvVbatLimitLower))
                {
                    flag_ng = 0;

                    // update UI
                    mainForm.gMeasValCvVbat = measHvCvVbatCurrent; // 170306
                    mainForm.showCcCurrentResult(measHvCvVbatCurrent, 2, true);
                }
                else
                {
                    flag_ng = 1;

                    // update UI
                    mainForm.gMeasValCvVbat = measHvCvVbatCurrent; // 170306
                    mainForm.showCcCurrentResult(measHvCvVbatCurrent, 2, false);
                }
            }

            //mainForm.showCcCurrentResult(mainForm.getCcCurrent());

            mainForm.setSimulatorCh2Off();
            Thread.Sleep(500);
            //mainForm.setSimulatorCh1Off();
            //Thread.Sleep(200);


                mainForm.setSimulatorCh2Off();
                Thread.Sleep(500);
                mainForm.setSimulatorCh1Off();
                Thread.Sleep(500);

                double V = 0;

                if (flag_ng == 1)
                {
                    // display result
                    mainForm.updateThmStatus(V, 4, false);
                }
                else
                {
                    mainForm.updateThmStatus(V, 4, true);
                }

                mainForm.toggleBtn(true);
            
        }

    }
}