using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EdgeSim.Rules.Core;
using EdgeSim.Rules.Enum;
using System.Windows.Forms.DataVisualization.Charting;

namespace EdgeSim.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tbTarget_Scroll(null, null);
        }

        private void tbTarget_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                chart1.Series[i].Points.Clear();
                chart2.Series[i].Points.Clear();
            }

            for (byte lSkill = 0; lSkill <= 20; lSkill++)
            {
                Double[] lResult = { 0, 0, 0, 0, 0 };

                for (int lTestNo = 0; lTestNo < 1000; lTestNo++)
                {
                    CheckResult lRes = CheckRoll.ActionCheck(5, lSkill, (byte)tbCaution.Value, (byte)tbTarget.Value, (byte)tbRounds.Value);

                    lResult[(int)lRes] += 1;
                }

                for (int i = 0; i < 5; i++)
                    chart1.Series[i].Points.AddXY(lSkill, lResult[i]);
            }

            for (byte lAttribute = 1; lAttribute <= 10; lAttribute++)
            {
                Double[] lResult = { 0, 0, 0, 0, 0 };

                for (int lTestNo = 0; lTestNo < 1000; lTestNo++)
                {
                    CheckResult lRes = CheckRoll.ActionCheck(lAttribute, 5, (byte)tbCaution.Value, (byte)tbTarget.Value, (byte)tbRounds.Value);

                    lResult[(int)lRes] += 1;
                }

                for (int i = 0; i < 5; i++)
                    chart2.Series[i].Points.AddXY(lAttribute, lResult[i]);
            }
        }
    }
}
