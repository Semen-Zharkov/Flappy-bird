namespace FlappyBird
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            wasted = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // wasted
            // 
            wasted.FlatAppearance.BorderColor = Color.Black;
            wasted.FlatStyle = FlatStyle.Flat;
            wasted.Font = new Font("Showcard Gothic", 14.2F, FontStyle.Regular, GraphicsUnit.Point);
            wasted.ForeColor = Color.Black;
            wasted.Location = new Point(195, 223);
            wasted.Name = "wasted";
            wasted.Size = new Size(206, 39);
            wasted.TabIndex = 0;
            wasted.Text = "Начать заного?";
            wasted.UseVisualStyleBackColor = true;
            wasted.UseWaitCursor = true;
            wasted.Click += button1_Click;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(605, 477);
            Controls.Add(wasted);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            KeyPreview = true;
            Margin = new Padding(5);
            Name = "Form1";
            Text = "Flappy Bird";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Paint += OnPaint;
            KeyDown += Form1_KeyDown;
            MouseDown += Form1_MouseDown;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private Button wasted;
        private Label label1;
    }
}