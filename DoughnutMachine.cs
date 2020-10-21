using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Threading;

namespace Marian_Bianca_Lab2
{   //lab3part1
    class DoughnutMachine : Component//lab3part5
    {
        //lab3part4
        private DoughnutType mFlavor;
        
        public DoughnutType Flavor
        {
            get
            {
                return mFlavor;
            }
            set
            {
                mFlavor = value;
            }
        }
        //lab3part5
        private System.Collections.ArrayList mDoughnuts = new System.Collections.ArrayList();
        public Doughnut this[int Index]
        {
            get
            {
                return (Doughnut)mDoughnuts[Index];
            }
            set
            {
                mDoughnuts[Index] = value;
            }
        }
        //lab3part6
        public delegate void DoughnutCompleteDelegate();
        public event DoughnutCompleteDelegate DoughnutComplete;
        //lab3part7
        DispatcherTimer doughnutTimer;
        //lab3part8
        private void InitializeComponent()
        {
            this.doughnutTimer = new DispatcherTimer();
            this.doughnutTimer.Tick += new System.EventHandler(this.doughnutTimer_Tick);
        }
        //lab3part9
        public DoughnutMachine()
        {
            InitializeComponent();
        }
        //lab3part10
        private void doughnutTimer_Tick(object sender, EventArgs e)
        {
            Doughnut aDoughnut = new Doughnut(this.Flavor);
            mDoughnuts.Add(aDoughnut);
            DoughnutComplete();
        }
        //lab3part11
        public bool Enabled
        {
            set
            {
                doughnutTimer.IsEnabled = value;
            }

        }
        public int Interval
        {
            set
            {
                doughnutTimer.Interval = new TimeSpan(0, 0, value);
            }
        }
        //lab3part12
        public void MakeDoughnuts(DoughnutType dFlavor)
        {
            Flavor = dFlavor;
            switch (Flavor)
            {
                case DoughnutType.Glazed:Interval = 3;break;
                case DoughnutType.Sugar:Interval = 2;break;
                case DoughnutType.Lemon:Interval = 5;break;
                case DoughnutType.Vanilla:Interval = 4;break;
                case DoughnutType.Chocolate: Interval = 7; break;
               
            }
            doughnutTimer.Start();
        }
    }
    //lab3part2
    public enum DoughnutType
    {
        Glazed,
        Sugar,
        Lemon,
        Chocolate,
        Vanilla
    }
    //lab3part3
    class Doughnut
    {
        private DoughnutType mFlavor;

        public DoughnutType Flavor
        {

            get
            {

                return mFlavor;
            
            }
            set
            {

                mFlavor = value;
           
            }
        }
        private float mPrice = .50F;
        public float Price
        {
            get
            {
                
                return mPrice;

            }
            set
            {

                mPrice = value;
            
            }
        }
        private readonly DateTime mTimeOfCreation;
        public DateTime TimeOfCreation
        {
            get
            {
                return mTimeOfCreation;
            }
            
        }
        public Doughnut(DoughnutType aFlavor) //constructor
        {
            mTimeOfCreation = DateTime.Now;
            mFlavor = aFlavor;
        }

    }
}
