using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

public class Form1 : Form
{
	public Form1()
	{
        this.InitializeComponent();
        //this.AutoScaleBaseSize = new Size(6, 14);
        //this.ClientSize = new Size(400, 300);
        this.PaintArea.Paint += new PaintEventHandler(this.Form1_Paint);
    }
	static void Main() 
	{
		Application.Run(new Form1());
	}
	private void Form1_Paint(object sender, PaintEventArgs e)
	{
		graphics = e.Graphics ;
        try
        {
            drawCayleyTree(Int32.Parse(textBox_depth.Text), PaintArea.Size.Width/2/*400*/, PaintArea.Size.Height-50, Int32.Parse(textBox_leng.Text), -Math.PI/2 );//the second and the third arg should refer to the paint area's localtion
        }
        catch(Exception err)//catch invalid arguments
        {
            MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
	}

	private Graphics graphics;
    private Label lb_th1;
    private Label lb_th2;
    private Label lb_per1;
    private GroupBox PaintArea;
    private Panel ControlArea;
    private TableLayoutPanel tableLayoutPanel1;
    private Label lb_color;
    private Label lb_leng;
    private TextBox textBox_depth;
    private Label lb_depth;
    private ListBox listBox1_color;
    private TextBox textBox_leng;
    private TextBox textBox_per1;
    private TextBox textBox_per2;
    private TextBox textBox_th1;
    private TextBox textBox_th2;
    private Button button_draw;
    private Label lb_per2;

    void drawCayleyTree(int n, 
			double x0, double y0, double leng, double th)
	{
        if (n == 0) return;

        double x1 = x0 + leng * Math.Cos(th);
        double y1 = y0 + leng * Math.Sin(th);

        drawLine(x0, y0, x1, y1);

        drawCayleyTree(n - 1, x1, y1, Double.Parse(textBox_per1.Text) * Double.Parse(textBox_leng.Text), th + Double.Parse(textBox_th1.Text));
        drawCayleyTree(n - 1, x1, y1, Double.Parse(textBox_per2.Text) * Double.Parse(textBox_leng.Text), th - Double.Parse(textBox_th2.Text));
    }
	void drawLine( double x0, double y0, double x1, double y1 ){
        Dictionary<int, Pen> PenColor = new();
        PenColor.Add(0, Pens.Red);
        PenColor.Add(1, Pens.Blue);
        PenColor.Add(2, Pens.Green);
		graphics.DrawLine( 
			PenColor[listBox1_color.SelectedIndex],
            
			(int)x0, (int)y0, (int)x1, (int)y1 );
	}

    private void InitializeComponent()
    {
            this.PaintArea = new System.Windows.Forms.GroupBox();
            this.ControlArea = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_color = new System.Windows.Forms.Label();
            this.lb_th2 = new System.Windows.Forms.Label();
            this.lb_th1 = new System.Windows.Forms.Label();
            this.lb_per2 = new System.Windows.Forms.Label();
            this.lb_per1 = new System.Windows.Forms.Label();
            this.lb_leng = new System.Windows.Forms.Label();
            this.textBox_depth = new System.Windows.Forms.TextBox();
            this.textBox_per1 = new System.Windows.Forms.TextBox();
            this.textBox_th1 = new System.Windows.Forms.TextBox();
            this.textBox_leng = new System.Windows.Forms.TextBox();
            this.textBox_per2 = new System.Windows.Forms.TextBox();
            this.textBox_th2 = new System.Windows.Forms.TextBox();
            this.lb_depth = new System.Windows.Forms.Label();
            this.button_draw = new System.Windows.Forms.Button();
            this.listBox1_color = new System.Windows.Forms.ListBox();
            this.ControlArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PaintArea
            // 
            this.PaintArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaintArea.Location = new System.Drawing.Point(0, 0);
            this.PaintArea.Name = "PaintArea";
            this.PaintArea.Size = new System.Drawing.Size(787, 578);
            this.PaintArea.TabIndex = 2;
            this.PaintArea.TabStop = false;
            this.PaintArea.Text = "groupBox1";
            this.PaintArea.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ControlArea
            // 
            this.ControlArea.Controls.Add(this.tableLayoutPanel1);
            this.ControlArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlArea.Location = new System.Drawing.Point(0, 0);
            this.ControlArea.Name = "ControlArea";
            this.ControlArea.Size = new System.Drawing.Size(787, 298);
            this.ControlArea.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.lb_color, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lb_th2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lb_th1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lb_per2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_per1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_leng, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_depth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_per1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_th1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_leng, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_per2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_th2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lb_depth, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_draw, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.listBox1_color, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(787, 298);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lb_color
            // 
            this.lb_color.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_color.AutoSize = true;
            this.lb_color.Location = new System.Drawing.Point(78, 250);
            this.lb_color.Name = "lb_color";
            this.lb_color.Size = new System.Drawing.Size(39, 20);
            this.lb_color.TabIndex = 14;
            this.lb_color.Text = "颜色";
            // 
            // lb_th2
            // 
            this.lb_th2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_th2.AutoSize = true;
            this.lb_th2.Location = new System.Drawing.Point(448, 175);
            this.lb_th2.Name = "lb_th2";
            this.lb_th2.Size = new System.Drawing.Size(84, 20);
            this.lb_th2.TabIndex = 13;
            this.lb_th2.Text = "左分支角度";
            // 
            // lb_th1
            // 
            this.lb_th1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_th1.AutoSize = true;
            this.lb_th1.Location = new System.Drawing.Point(56, 175);
            this.lb_th1.Name = "lb_th1";
            this.lb_th1.Size = new System.Drawing.Size(84, 20);
            this.lb_th1.TabIndex = 12;
            this.lb_th1.Text = "右分支角度";
            // 
            // lb_per2
            // 
        this.lb_per2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_per2.AutoSize = true;
            this.lb_per2.Location = new System.Drawing.Point(440, 101);
            this.lb_per2.Name = "lb_per2";
            this.lb_per2.Size = new System.Drawing.Size(99, 20);
            this.lb_per2.TabIndex = 11;
            this.lb_per2.Text = "左分支长度比";
            // 
            // lb_per1
            // 
            this.lb_per1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_per1.AutoSize = true;
            this.lb_per1.Location = new System.Drawing.Point(48, 101);
            this.lb_per1.Name = "lb_per1";
            this.lb_per1.Size = new System.Drawing.Size(99, 20);
            this.lb_per1.TabIndex = 10;
            this.lb_per1.Text = "右分支长度比";
            // 
            // lb_leng
            // 
            this.lb_leng.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_leng.AutoSize = true;
            this.lb_leng.Location = new System.Drawing.Point(455, 27);
            this.lb_leng.Name = "lb_leng";
            this.lb_leng.Size = new System.Drawing.Size(69, 20);
            this.lb_leng.TabIndex = 9;
            this.lb_leng.Text = "主干长度";
            // 
            // textBox_depth
            // 
            this.textBox_depth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_depth.Location = new System.Drawing.Point(231, 23);
            this.textBox_depth.Name = "textBox_depth";
            this.textBox_depth.Size = new System.Drawing.Size(125, 27);
            this.textBox_depth.TabIndex = 0;
            this.textBox_depth.Text = "3";
            // 
            // textBox_per1
            // 
            this.textBox_per1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_per1.Location = new System.Drawing.Point(231, 97);
            this.textBox_per1.Name = "textBox_per1";
            this.textBox_per1.Size = new System.Drawing.Size(125, 27);
            this.textBox_per1.TabIndex = 1;
            this.textBox_per1.Text = "0.6";
            // 
            // textBox_th1
            // 
            this.textBox_th1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_th1.Location = new System.Drawing.Point(231, 171);
            this.textBox_th1.Name = "textBox_th1";
            this.textBox_th1.Size = new System.Drawing.Size(125, 27);
            this.textBox_th1.TabIndex = 2;
            this.textBox_th1.Text = "0.5";
            // 
            // textBox_leng
            // 
        this.textBox_leng.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_leng.Location = new System.Drawing.Point(625, 23);
            this.textBox_leng.Name = "textBox_leng";
            this.textBox_leng.Size = new System.Drawing.Size(125, 27);
            this.textBox_leng.TabIndex = 5;
            this.textBox_leng.Text = "100";
            // 
            // textBox_per2
            // 
            this.textBox_per2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_per2.Location = new System.Drawing.Point(625, 97);
            this.textBox_per2.Name = "textBox_per2";
            this.textBox_per2.Size = new System.Drawing.Size(125, 27);
            this.textBox_per2.TabIndex = 6;
            this.textBox_per2.Text = "0.7";
            // 
            // textBox_th2
            // 
            this.textBox_th2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_th2.Location = new System.Drawing.Point(625, 171);
            this.textBox_th2.Name = "textBox_th2";
            this.textBox_th2.Size = new System.Drawing.Size(125, 27);
            this.textBox_th2.TabIndex = 7;
            this.textBox_th2.Text = "0.6";
            // 
            // lb_depth
            // 
            this.lb_depth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_depth.AutoSize = true;
            this.lb_depth.Location = new System.Drawing.Point(63, 27);
            this.lb_depth.Name = "lb_depth";
            this.lb_depth.Size = new System.Drawing.Size(69, 20);
            this.lb_depth.TabIndex = 8;
            this.lb_depth.Text = "递归深度";
            // 
            // button_draw
            // 
            this.button_draw.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_draw.Location = new System.Drawing.Point(640, 245);
            this.button_draw.Name = "button_draw";
            this.button_draw.Size = new System.Drawing.Size(94, 29);
            this.button_draw.TabIndex = 15;
            this.button_draw.Text = "button1";
            this.button_draw.UseVisualStyleBackColor = true;
            // 
            // listBox1_color
            // 
            this.listBox1_color.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBox1_color.FormattingEnabled = true;
            this.listBox1_color.ItemHeight = 20;
            this.listBox1_color.Items.AddRange(new object[] {
            "red",
            "gree",
            "blue"});
            this.listBox1_color.Location = new System.Drawing.Point(219, 228);
            this.listBox1_color.Name = "listBox1_color";
            this.listBox1_color.Size = new System.Drawing.Size(150, 64);
            this.listBox1_color.TabIndex = 16;
            this.listBox1_color.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1_color.SelectedIndex = 0;//select red by default
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(787, 578);
            this.Controls.Add(this.ControlArea);
            this.Controls.Add(this.PaintArea);
            this.Name = "Form1";
            this.ControlArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {

    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//
    {

    }
}
