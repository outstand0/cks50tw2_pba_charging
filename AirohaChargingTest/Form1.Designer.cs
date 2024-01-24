namespace AirohaChargingTest
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tb_LvCcCurrentAtVbat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_HvCvCurrentAtVbat = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_lv_current_vbus_upper = new System.Windows.Forms.Label();
            this.lb_lv_current_vbus_lower = new System.Windows.Forms.Label();
            this.lb_lv_current_vbat_upper = new System.Windows.Forms.Label();
            this.lb_lv_current_vbat_lower = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_LvCcCurrentAtVbus = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_hv_current_vbus_upper = new System.Windows.Forms.Label();
            this.lb_hv_current_vbus_lower = new System.Windows.Forms.Label();
            this.lb_hv_current_vbat_upper = new System.Windows.Forms.Label();
            this.lb_hv_current_vbat_lower = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_HvCvCurrentAtVbus = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbResult = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lb_lv_cc2_current_vbus_upper = new System.Windows.Forms.Label();
            this.lb_lv_cc2_current_vbus_lower = new System.Windows.Forms.Label();
            this.lb_lv_cc2_current_vbat_upper = new System.Windows.Forms.Label();
            this.lb_lv_cc2_current_vbat_lower = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_LvCC2CcCurrentAtVbus = new System.Windows.Forms.TextBox();
            this.tb_LvCC2CcCurrentAtVbat = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current at Vbat (A)";
            // 
            // tb_LvCcCurrentAtVbat
            // 
            this.tb_LvCcCurrentAtVbat.Location = new System.Drawing.Point(230, 27);
            this.tb_LvCcCurrentAtVbat.Name = "tb_LvCcCurrentAtVbat";
            this.tb_LvCcCurrentAtVbat.Size = new System.Drawing.Size(100, 21);
            this.tb_LvCcCurrentAtVbat.TabIndex = 3;
            this.tb_LvCcCurrentAtVbat.TabStop = false;
            this.tb_LvCcCurrentAtVbat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current at Vbat (A)";
            // 
            // tb_HvCvCurrentAtVbat
            // 
            this.tb_HvCvCurrentAtVbat.Location = new System.Drawing.Point(230, 30);
            this.tb_HvCvCurrentAtVbat.Name = "tb_HvCvCurrentAtVbat";
            this.tb_HvCvCurrentAtVbat.Size = new System.Drawing.Size(100, 21);
            this.tb_HvCvCurrentAtVbat.TabIndex = 3;
            this.tb_HvCvCurrentAtVbat.TabStop = false;
            this.tb_HvCvCurrentAtVbat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_lv_current_vbus_upper);
            this.groupBox1.Controls.Add(this.lb_lv_current_vbus_lower);
            this.groupBox1.Controls.Add(this.lb_lv_current_vbat_upper);
            this.groupBox1.Controls.Add(this.lb_lv_current_vbat_lower);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_LvCcCurrentAtVbus);
            this.groupBox1.Controls.Add(this.tb_LvCcCurrentAtVbat);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 113);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Low Voltage(CC1 - 3.0V)";
            // 
            // lb_lv_current_vbus_upper
            // 
            this.lb_lv_current_vbus_upper.AutoSize = true;
            this.lb_lv_current_vbus_upper.Location = new System.Drawing.Point(348, 68);
            this.lb_lv_current_vbus_upper.Name = "lb_lv_current_vbus_upper";
            this.lb_lv_current_vbus_upper.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_current_vbus_upper.TabIndex = 4;
            this.lb_lv_current_vbus_upper.Text = "label5";
            // 
            // lb_lv_current_vbus_lower
            // 
            this.lb_lv_current_vbus_lower.AutoSize = true;
            this.lb_lv_current_vbus_lower.Location = new System.Drawing.Point(170, 68);
            this.lb_lv_current_vbus_lower.Name = "lb_lv_current_vbus_lower";
            this.lb_lv_current_vbus_lower.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_current_vbus_lower.TabIndex = 4;
            this.lb_lv_current_vbus_lower.Text = "label5";
            // 
            // lb_lv_current_vbat_upper
            // 
            this.lb_lv_current_vbat_upper.AutoSize = true;
            this.lb_lv_current_vbat_upper.Location = new System.Drawing.Point(348, 30);
            this.lb_lv_current_vbat_upper.Name = "lb_lv_current_vbat_upper";
            this.lb_lv_current_vbat_upper.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_current_vbat_upper.TabIndex = 4;
            this.lb_lv_current_vbat_upper.Text = "label5";
            // 
            // lb_lv_current_vbat_lower
            // 
            this.lb_lv_current_vbat_lower.AutoSize = true;
            this.lb_lv_current_vbat_lower.Location = new System.Drawing.Point(170, 30);
            this.lb_lv_current_vbat_lower.Name = "lb_lv_current_vbat_lower";
            this.lb_lv_current_vbat_lower.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_current_vbat_lower.TabIndex = 4;
            this.lb_lv_current_vbat_lower.Text = "label5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Current at Vbus (A)";
            // 
            // tb_LvCcCurrentAtVbus
            // 
            this.tb_LvCcCurrentAtVbus.Location = new System.Drawing.Point(230, 65);
            this.tb_LvCcCurrentAtVbus.Name = "tb_LvCcCurrentAtVbus";
            this.tb_LvCcCurrentAtVbus.Size = new System.Drawing.Size(100, 21);
            this.tb_LvCcCurrentAtVbus.TabIndex = 3;
            this.tb_LvCcCurrentAtVbus.TabStop = false;
            this.tb_LvCcCurrentAtVbus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_hv_current_vbus_upper);
            this.groupBox2.Controls.Add(this.lb_hv_current_vbus_lower);
            this.groupBox2.Controls.Add(this.lb_hv_current_vbat_upper);
            this.groupBox2.Controls.Add(this.lb_hv_current_vbat_lower);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_HvCvCurrentAtVbat);
            this.groupBox2.Controls.Add(this.tb_HvCvCurrentAtVbus);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 281);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(421, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "High Voltage (CV - 4.4V)";
            // 
            // lb_hv_current_vbus_upper
            // 
            this.lb_hv_current_vbus_upper.AutoSize = true;
            this.lb_hv_current_vbus_upper.Location = new System.Drawing.Point(348, 71);
            this.lb_hv_current_vbus_upper.Name = "lb_hv_current_vbus_upper";
            this.lb_hv_current_vbus_upper.Size = new System.Drawing.Size(38, 12);
            this.lb_hv_current_vbus_upper.TabIndex = 4;
            this.lb_hv_current_vbus_upper.Text = "label5";
            // 
            // lb_hv_current_vbus_lower
            // 
            this.lb_hv_current_vbus_lower.AutoSize = true;
            this.lb_hv_current_vbus_lower.Location = new System.Drawing.Point(170, 71);
            this.lb_hv_current_vbus_lower.Name = "lb_hv_current_vbus_lower";
            this.lb_hv_current_vbus_lower.Size = new System.Drawing.Size(38, 12);
            this.lb_hv_current_vbus_lower.TabIndex = 4;
            this.lb_hv_current_vbus_lower.Text = "label5";
            // 
            // lb_hv_current_vbat_upper
            // 
            this.lb_hv_current_vbat_upper.AutoSize = true;
            this.lb_hv_current_vbat_upper.Location = new System.Drawing.Point(348, 33);
            this.lb_hv_current_vbat_upper.Name = "lb_hv_current_vbat_upper";
            this.lb_hv_current_vbat_upper.Size = new System.Drawing.Size(38, 12);
            this.lb_hv_current_vbat_upper.TabIndex = 4;
            this.lb_hv_current_vbat_upper.Text = "label5";
            // 
            // lb_hv_current_vbat_lower
            // 
            this.lb_hv_current_vbat_lower.AutoSize = true;
            this.lb_hv_current_vbat_lower.Location = new System.Drawing.Point(170, 33);
            this.lb_hv_current_vbat_lower.Name = "lb_hv_current_vbat_lower";
            this.lb_hv_current_vbat_lower.Size = new System.Drawing.Size(38, 12);
            this.lb_hv_current_vbat_lower.TabIndex = 4;
            this.lb_hv_current_vbat_lower.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Current at Vbus (A)";
            // 
            // tb_HvCvCurrentAtVbus
            // 
            this.tb_HvCvCurrentAtVbus.Location = new System.Drawing.Point(230, 68);
            this.tb_HvCvCurrentAtVbus.Name = "tb_HvCvCurrentAtVbus";
            this.tb_HvCvCurrentAtVbus.Size = new System.Drawing.Size(100, 21);
            this.tb_HvCvCurrentAtVbus.TabIndex = 3;
            this.tb_HvCvCurrentAtVbus.TabStop = false;
            this.tb_HvCvCurrentAtVbus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("굴림", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.Location = new System.Drawing.Point(12, 449);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(789, 83);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbResult
            // 
            this.lbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbResult.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbResult.Location = new System.Drawing.Point(439, 12);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(362, 369);
            this.lbResult.TabIndex = 7;
            this.lbResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.lb_lv_cc2_current_vbus_upper);
            this.groupBox3.Controls.Add(this.lb_lv_cc2_current_vbus_lower);
            this.groupBox3.Controls.Add(this.lb_lv_cc2_current_vbat_upper);
            this.groupBox3.Controls.Add(this.lb_lv_cc2_current_vbat_lower);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.tb_LvCC2CcCurrentAtVbus);
            this.groupBox3.Controls.Add(this.tb_LvCC2CcCurrentAtVbat);
            this.groupBox3.Location = new System.Drawing.Point(12, 142);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(421, 113);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Low Voltage(CC2 - 4.0V)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "Current at Vbat (A)";
            // 
            // lb_lv_cc2_current_vbus_upper
            // 
            this.lb_lv_cc2_current_vbus_upper.AutoSize = true;
            this.lb_lv_cc2_current_vbus_upper.Location = new System.Drawing.Point(348, 68);
            this.lb_lv_cc2_current_vbus_upper.Name = "lb_lv_cc2_current_vbus_upper";
            this.lb_lv_cc2_current_vbus_upper.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_cc2_current_vbus_upper.TabIndex = 4;
            this.lb_lv_cc2_current_vbus_upper.Text = "label5";
            // 
            // lb_lv_cc2_current_vbus_lower
            // 
            this.lb_lv_cc2_current_vbus_lower.AutoSize = true;
            this.lb_lv_cc2_current_vbus_lower.Location = new System.Drawing.Point(170, 68);
            this.lb_lv_cc2_current_vbus_lower.Name = "lb_lv_cc2_current_vbus_lower";
            this.lb_lv_cc2_current_vbus_lower.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_cc2_current_vbus_lower.TabIndex = 4;
            this.lb_lv_cc2_current_vbus_lower.Text = "label5";
            // 
            // lb_lv_cc2_current_vbat_upper
            // 
            this.lb_lv_cc2_current_vbat_upper.AutoSize = true;
            this.lb_lv_cc2_current_vbat_upper.Location = new System.Drawing.Point(348, 30);
            this.lb_lv_cc2_current_vbat_upper.Name = "lb_lv_cc2_current_vbat_upper";
            this.lb_lv_cc2_current_vbat_upper.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_cc2_current_vbat_upper.TabIndex = 4;
            this.lb_lv_cc2_current_vbat_upper.Text = "label5";
            // 
            // lb_lv_cc2_current_vbat_lower
            // 
            this.lb_lv_cc2_current_vbat_lower.AutoSize = true;
            this.lb_lv_cc2_current_vbat_lower.Location = new System.Drawing.Point(170, 30);
            this.lb_lv_cc2_current_vbat_lower.Name = "lb_lv_cc2_current_vbat_lower";
            this.lb_lv_cc2_current_vbat_lower.Size = new System.Drawing.Size(38, 12);
            this.lb_lv_cc2_current_vbat_lower.TabIndex = 4;
            this.lb_lv_cc2_current_vbat_lower.Text = "label5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "Current at Vbus (A)";
            // 
            // tb_LvCC2CcCurrentAtVbus
            // 
            this.tb_LvCC2CcCurrentAtVbus.Location = new System.Drawing.Point(230, 65);
            this.tb_LvCC2CcCurrentAtVbus.Name = "tb_LvCC2CcCurrentAtVbus";
            this.tb_LvCC2CcCurrentAtVbus.Size = new System.Drawing.Size(100, 21);
            this.tb_LvCC2CcCurrentAtVbus.TabIndex = 3;
            this.tb_LvCC2CcCurrentAtVbus.TabStop = false;
            this.tb_LvCC2CcCurrentAtVbus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_LvCC2CcCurrentAtVbat
            // 
            this.tb_LvCC2CcCurrentAtVbat.Location = new System.Drawing.Point(230, 27);
            this.tb_LvCC2CcCurrentAtVbat.Name = "tb_LvCC2CcCurrentAtVbat";
            this.tb_LvCC2CcCurrentAtVbat.Size = new System.Drawing.Size(100, 21);
            this.tb_LvCC2CcCurrentAtVbat.TabIndex = 3;
            this.tb_LvCC2CcCurrentAtVbat.TabStop = false;
            this.tb_LvCC2CcCurrentAtVbat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 574);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_LvCcCurrentAtVbat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_HvCvCurrentAtVbat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_LvCcCurrentAtVbus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_HvCvCurrentAtVbus;
        private System.Windows.Forms.Label lb_lv_current_vbus_upper;
        private System.Windows.Forms.Label lb_lv_current_vbus_lower;
        private System.Windows.Forms.Label lb_lv_current_vbat_upper;
        private System.Windows.Forms.Label lb_lv_current_vbat_lower;
        private System.Windows.Forms.Label lb_hv_current_vbus_upper;
        private System.Windows.Forms.Label lb_hv_current_vbus_lower;
        private System.Windows.Forms.Label lb_hv_current_vbat_upper;
        private System.Windows.Forms.Label lb_hv_current_vbat_lower;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lb_lv_cc2_current_vbus_upper;
        private System.Windows.Forms.Label lb_lv_cc2_current_vbus_lower;
        private System.Windows.Forms.Label lb_lv_cc2_current_vbat_upper;
        private System.Windows.Forms.Label lb_lv_cc2_current_vbat_lower;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_LvCC2CcCurrentAtVbus;
        private System.Windows.Forms.TextBox tb_LvCC2CcCurrentAtVbat;
    }
}

