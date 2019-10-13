using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Cm_1
{
    public partial class Form1 : Form
    {
        public int colortype = 0;
        public Form1()
        {
            InitializeComponent();
            label14.Enabled = false;
            textBox11.Enabled = false;
            label23.Enabled = false;
            label24.Enabled = false;
            label32.Enabled = false;
            label33.Enabled = false;
            zedGraphControl1.GraphPane.XAxis.Title = "Ось X";
            zedGraphControl1.GraphPane.YAxis.Title = "Ось U";
            zedGraphControl1.GraphPane.Title = "График зависимости U(X)";
            zedGraphControl2.GraphPane.XAxis.Title = "Ось X";
            zedGraphControl2.GraphPane.YAxis.Title = "Ось U'";
            zedGraphControl2.GraphPane.Title = "График зависимости U'(X)";
            zedGraphControl3.GraphPane.XAxis.Title = "Ось U";
            zedGraphControl3.GraphPane.YAxis.Title = "Ось U'";
            zedGraphControl3.GraphPane.Title = "График зависимости U'(U)";
        }

        private void checkBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label14.Enabled = true;
                textBox11.Enabled = true;
                label23.Enabled = true;
                label24.Enabled = true;
                label32.Enabled = true;
                label33.Enabled = true;
            }
            else
            {
                label14.Enabled = false;
                textBox11.Enabled = false;
                label23.Enabled = false;
                label24.Enabled = false;
                label32.Enabled = false;
                label33.Enabled = false;
            }
        }

        private void DrawGraph(ZedGraphControl pane, ref double[] x, ref double[] v, int colortype)
        {
            GraphPane Pane = pane.GraphPane;
            PointPairList list = new PointPairList();
            for (int i = 0; i < x.Length; i++)
            {
                list.Add(x[i], v[i]);
            }
            if (colortype == 0)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Red, SymbolType.None);
            }
            if (colortype == 1)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Blue, SymbolType.None);
            }
            if (colortype == 2)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Black, SymbolType.None);
            }
            if (colortype == 3)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Purple, SymbolType.None);
            }
            if (colortype == 4)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Green, SymbolType.None);
            }
            if (colortype == 5)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Gold, SymbolType.None);
            }
            if (colortype == 6)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Violet, SymbolType.None);
            }
            if (colortype == 7)
            {
                LineItem curve = Pane.AddCurve("", list, Color.Brown, SymbolType.None);
            }
            pane.GraphPane.XAxis.MinAuto = true;
            pane.GraphPane.YAxis.MinAuto = true;
            pane.GraphPane.XAxis.MaxAuto = true;
            pane.GraphPane.YAxis.MaxAuto = true;
            pane.AxisChange();
            pane.Invalidate();
        }

        private double func1 (double m, double c, double k, double k_, double u, double u_)
        {
            return -(c / m * u_ + k / m * u + k_ / m * Math.Pow(u, 3));
        }

        private double func2(double u_)
        {
            return u_;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            double m = Convert.ToDouble(textBox1.Text);
            double c = Convert.ToDouble(textBox2.Text);
            double k = Convert.ToDouble(textBox3.Text);
            double k_ = Convert.ToDouble(textBox4.Text);
            double Step_now = Convert.ToDouble(textBox5.Text);
            int Number_steps = Convert.ToInt32(textBox6.Text);
            double b = Convert.ToDouble(textBox7.Text);
            double[] x = new double[Number_steps + 1];
            double[] v = new double[Number_steps + 1];
            double[] v_ = new double[Number_steps + 1];
            x[0] = Convert.ToDouble(textBox8.Text);
            v[0] = Convert.ToDouble(textBox9.Text);
            v_[0] = Convert.ToDouble(textBox10.Text);
            double x_max = Convert.ToDouble(textBox12.Text);
            double[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            result[2] = Step_now;
            result[3] = x[0];
            result[4] = Step_now;
            result[5] = x[0];

            if (checkBox1.Checked == false)
            {
                for (int i = 0; i < Number_steps; i++)
                {
                    x[i + 1] = x[i] + Step_now;
                    if (x[i + 1] > x_max + b)
                    {
                        result[1] = x_max + b - x[i];
                        break;
                    }
                    double k1 = func1(m, c, k, k_, v[i], v_[i]);
                    double l1 = func2(v_[i]);
                    double k2 = func1(m, c, k, k_, v[i] + Step_now / 2 * k1, v_[i] + Step_now / 2 * l1);
                    double l2 = func2(v_[i] + Step_now / 2 * l1);
                    double k3 = func1(m, c, k, k_, v[i] + Step_now / 2 * k2, v_[i] + Step_now / 2 * l2);
                    double l3 = func2(v_[i] + Step_now / 2 * l2);
                    double k4 = func1(m, c, k, k_, v[i] + Step_now * k3, v_[i] + Step_now * l3);
                    double l4 = func2(v_[i] + Step_now * l3);
                    v[i + 1] = v[i] + Step_now / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
                    v_[i + 1] = v_[i] + Step_now / 6 * (l1 + 2 * l2 + 2 * l3 + l4);
                    result[0] = i + 1;
                    dataGridView1.Rows.Add(i + 1, x[i + 1], v[i + 1], v_[i + 1], Step_now, '-', '-', '-', '-', '-', '-', '-');
                }
                DrawGraph(zedGraphControl1, ref x, ref v, colortype);
                DrawGraph(zedGraphControl2, ref x, ref v_, colortype);
                DrawGraph(zedGraphControl3, ref v, ref v_, colortype);
                colortype++;
            }
            else
            {
                double Local_error = Convert.ToDouble(textBox11.Text);
                double v_1_2;
                double v_1_2_;
                double v_1;
                double v_1_;
                int Num_del = 0;
                int Num_dou = 0;
                for (int i = 0; i < Number_steps; i++)
                {
                    x[i + 1] = x[i] + Step_now;
                    if (x[i + 1] > x_max + b)
                    {
                        result[1] = x_max + b - x[i];
                        result[7] = Num_dou;
                        result[8] = Num_del;
                        break;
                    }
                    double k1 = func1(m, c, k, k_, v[i], v_[i]);
                    double l1 = func2(v_[i]);
                    double k2 = func1(m, c, k, k_, v[i] + Step_now / 2 * k1, v_[i] + Step_now / 2 * l1);
                    double l2 = func2(v_[i] + Step_now / 2 * l1);
                    double k3 = func1(m, c, k, k_, v[i] + Step_now / 2 * k2, v_[i] + Step_now / 2 * l2);
                    double l3 = func2(v_[i] + Step_now / 2 * l2);
                    double k4 = func1(m, c, k, k_, v[i] + Step_now * k3, v_[i] + Step_now * l3);
                    double l4 = func2(v_[i] + Step_now * l3);
                    v[i + 1] = v[i] + Step_now / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
                    v_[i + 1] = v_[i] + Step_now / 6 * (l1 + 2 * l2 + 2 * l3 + l4);
                    result[0] = i + 1;

                    double k1_2 = func1(m, c, k, k_, v[i], v_[i]);
                    double l1_2 = func2(v_[i]);
                    double k2_2 = func1(m, c, k, k_, v[i] + Step_now / 2 / 2 * k1_2, v_[i] + Step_now / 2 / 2 * l1_2);
                    double l2_2 = func2(v_[i] + Step_now / 2 / 2 * l1_2);
                    double k3_2 = func1(m, c, k, k_, v[i] + Step_now / 2 / 2 * k2_2, v_[i] + Step_now / 2 / 2 * l2_2);
                    double l3_2 = func2(v_[i] + Step_now / 2 / 2 * l2_2);
                    double k4_2 = func1(m, c, k, k_, v[i] + Step_now / 2 * k3_2, v_[i] + Step_now / 2 * l3_2);
                    double l4_2 = func2(v_[i] + Step_now / 2 * l3_2);
                    v_1_2 = v[i] + Step_now / 2 / 6 * (k1_2 + 2 * k2_2 + 2 * k3_2 + k4_2);
                    v_1_2_ = v_[i] + Step_now / 2 / 6 * (l1_2 + 2 * l2_2 + 2 * l3_2 + l4_2);

                    double k1_ = func1(m, c, k, k_, v_1_2, v_1_2_);
                    double l1_ = func2(v_1_2_);
                    double k2_ = func1(m, c, k, k_, v_1_2 + Step_now / 2 / 2 * k1_, v_1_2_ + Step_now / 2 / 2 * l1_);
                    double l2_ = func2(v_1_2_ + Step_now / 2 / 2 * l1_);
                    double k3_ = func1(m, c, k, k_, v_1_2 + Step_now / 2 / 2 * k2_, v_1_2_ + Step_now / 2 / 2 * l2_);
                    double l3_ = func2(v_1_2_ + Step_now / 2 / 2 * l2_);
                    double k4_ = func1(m, c, k, k_, v_1_2 + Step_now / 2 * k3_, v_1_2_ + Step_now / 2 * l3_);
                    double l4_ = func2(v_1_2_ + Step_now / 2 * l3_);
                    v_1 = v_1_2 + Step_now / 2 / 6 * (k1_ + 2 * k2_ + 2 * k3_ + k4_);
                    v_1_ = v_1_2_ + Step_now / 2 / 6 * (l1_ + 2 * l2_ + 2 * l3_ + l4_);

                    double s1 = (v[i + 1] - v_1) / 15;
                    double s2 = (v[i + 1] - v_1_) / 15;
                    if (s1 < 0) s1 = -s1;
                    if (s2 < 0) s2 = -s2;
                    if (Math.Max(s1, s2) > Local_error)
                    {
                        Step_now = Step_now / 2;
                        Num_del++;
                        i--;
                    }
                    if (Math.Max(s1, s2) <= Local_error / 32)
                    {
                        Step_now = Step_now * 2;
                        dataGridView1.Rows.Add(i + 1, x[i + 1], v[i + 1], v_[i + 1], Step_now, Num_dou, Num_del, Math.Max(s1, s2), v_1, v_1_, s1 * 15, s2 * 15);
                        Num_dou++;
                    }
                    if (Math.Max(s1, s2) > Local_error / 32 && s1 + s2 <= Local_error)
                    {
                        dataGridView1.Rows.Add(i + 1, x[i + 1], v[i + 1], v_[i + 1], Step_now, Num_dou, Num_del, Math.Max(s1, s2), v_1, v_1_, s1 * 15, s2 * 15);
                    }

                    if (Step_now > result[2])
                    {
                        result[2] = Step_now;
                        result[3] = x[i + 1];
                    }
                    if (Step_now < result[4])
                    {
                        result[4] = Step_now;
                        result[5] = x[i + 1];
                    }
                    if (result[6] < Math.Max(s1, s2))
                    {
                        result[6] = Math.Max(s1, s2);
                    }
                }
                DrawGraph(zedGraphControl1, ref x, ref v, colortype);
                DrawGraph(zedGraphControl2, ref x, ref v_, colortype);
                DrawGraph(zedGraphControl3, ref v, ref v_, colortype);
                colortype++;
            }
            label25.Text = Convert.ToString(result[0]);
            label26.Text = Convert.ToString(result[1]);
            label27.Text = Convert.ToString(result[6]);
            label28.Text = Convert.ToString(result[2]);
            label29.Text = Convert.ToString(result[3]);
            label30.Text = Convert.ToString(result[4]);
            label31.Text = Convert.ToString(result[5]);
            label32.Text = Convert.ToString(result[7]);
            label33.Text = Convert.ToString(result[8]);
        }
    }
}
