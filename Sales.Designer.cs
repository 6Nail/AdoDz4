namespace Dz4
{
    partial class Sales
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
            this.SelectTables = new System.Windows.Forms.ComboBox();
            this.TextObject = new System.Windows.Forms.TextBox();
            this.LabelSelectTable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SelectTables
            // 
            this.SelectTables.FormattingEnabled = true;
            this.SelectTables.Items.AddRange(new object[] {
            "Покупатели",
            "Продавцы",
            "Продажи"});
            this.SelectTables.Location = new System.Drawing.Point(144, 79);
            this.SelectTables.Name = "SelectTables";
            this.SelectTables.Size = new System.Drawing.Size(121, 21);
            this.SelectTables.TabIndex = 0;
            this.SelectTables.SelectedIndexChanged += new System.EventHandler(this.SelectTables_SelectedIndexChanged);
            // 
            // TextObject
            // 
            this.TextObject.Location = new System.Drawing.Point(28, 121);
            this.TextObject.Multiline = true;
            this.TextObject.Name = "TextObject";
            this.TextObject.ReadOnly = true;
            this.TextObject.Size = new System.Drawing.Size(358, 279);
            this.TextObject.TabIndex = 1;
            this.TextObject.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelSelectTable
            // 
            this.LabelSelectTable.AutoSize = true;
            this.LabelSelectTable.Location = new System.Drawing.Point(154, 53);
            this.LabelSelectTable.Name = "LabelSelectTable";
            this.LabelSelectTable.Size = new System.Drawing.Size(100, 13);
            this.LabelSelectTable.TabIndex = 2;
            this.LabelSelectTable.Text = "Выберите таблицу";
            // 
            // Sales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 428);
            this.Controls.Add(this.LabelSelectTable);
            this.Controls.Add(this.TextObject);
            this.Controls.Add(this.SelectTables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Sales";
            this.Text = "Продажи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SelectTables;
        private System.Windows.Forms.TextBox TextObject;
        private System.Windows.Forms.Label LabelSelectTable;
    }
}

