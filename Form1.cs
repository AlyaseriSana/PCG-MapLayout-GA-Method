using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace MapProject
{
    public partial class Form1 : Form
    {
        
        Room room = new Room();
        
       // List<Tree> MapList = new List<Tree>();
        static int step1 = 50;
        static int step2 = 50;
        static int ImgW = 10; // the size of the map
        static int ImgH = 10; // the size of the map
        static int MutationRate = (int)(0.05 * (ImgH * ImgW));
        static int NumberOfShapTypes = 13;
        static int PubSize = 600;
        static int Maxloop = 200;
        private Tree bestGeneration = new Tree();
        static int crossPoint = (int)((ImgW)*(ImgH)) / 2 ;
        string message1 = "";
        Random rand = new Random();

        ExcelFile XtestFitness = new ExcelFile(@"C:\Users\ncd2763\Documents\Data\GA\test.xlsx", 1);


        static System.Random r = new System.Random();
        public int r1; public int r2;

        List<Tree> ChromosomesPopulation = new List<Tree>(PubSize);   // save all the chromosomes
        List<Tree> Offspring = new List<Tree>(PubSize);   // generation
        //Tree child1 = new Tree(NumberOfShapTypes, ImgW, ImgH);
       // Tree child2 = new Tree(NumberOfShapTypes, ImgW, ImgH);
        int row = 1;
        int col = 1;
       // Tree father;
       // Tree mother;
       
        public Form1()
        {
            InitializeComponent();
           // Paint += Form1_Paint;
        }

        private  void Form1_Load(object sender, EventArgs e)
        {
           
             System.Diagnostics.Debug.WriteLine(" Hellow ");
            XtestFitness.excelclear();
           
            row = 1;
            DoGA();
           // System.Diagnostics.Debug.WriteLine("Best generation of iteration " +  BestGeneration.FitnessValue );
            XtestFitness.ExcelSave();
            XtestFitness.excelClose();
          // MessageBox.Show(" the work is done on  " + BestGeneration.FitnessValue.ToString ());

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*
            
            TreeNode[] newNodes = new TreeNode[100];
            newNodes = bestGeneration.nodes;
            // Clear the form before drawing
            e.Graphics.Clear(Color.White);
            // draw the latest map
            int A = room.width + 2;
            bestGeneration.DrawGraph(e, A, step1, step2, ImgW, ImgH);
            //DrawGraphInForm(e, A, step1, step2, ImgW, ImgH,newNodes);
            bestGeneration.OnPaint(e);
            string message1 = " ";
            foreach (TreeNode node in bestGeneration.ListOfNodeResult)
                message1 += " + " + node.Data.ToString();
           //  MessageBox.Show(message1);
            message1 = " ";
            foreach (TreeNode node in bestGeneration.nodes)
                message1 += " + " + node.Size.ToString();
           // MessageBox.Show(message1);
           */
        }


        void DoGA()
        {
            string message3 = " the fitness = ";
            Tree BestMap = new Tree();
            // Build the poulation 
            ChromosomesPopulation.Clear();
           
            for (int i = 0; i < PubSize; i++)
            {
                Tree NT = new Tree(NumberOfShapTypes, ImgW, ImgH);
               
               // System.Diagnostics.Debug.WriteLine(" element is " + NT.FitnessValue+ " Listresultaaccount = "+ NT.getFitness());

                ChromosomesPopulation.Add(NT);
            }
            // end the population 

            /*
            int k1 = 0;
            do
            {
                Tree NT = new Tree(NumberOfShapTypes, ImgW, ImgH);
                NT.FitnessValue= NT.getFitness();
                System.Diagnostics.Debug.WriteLine(" k1 is " + k1 + " fitness is " + NT.FitnessValue);
                if ((NT.CalculateThePinaltyScoreStart() == 1) || (NT.CalculateThePinaltyScoreEnd()==1))
                {
                    System.Diagnostics.Debug.WriteLine(" k1 is " + k1 + " fitness is " + NT.FitnessValue);
                    ChromosomesPopulation.Add(NT);
                    k1++;
                }
                    
               
            } while (k1 < PubSize);

            */
           
            int k = 0;
            //string ChildMessage = " ";
            do
            {

                System.Diagnostics.Debug.WriteLine(" Do  GA  " + k);
                Offspring.Clear();
                int f1;
                int m1;
               // ChildMessage += "K= ("+k+")";
                for (int i = 0; i < PubSize; i++)
                {
                  
                    
                    f1 = GetParent(PubSize, ChromosomesPopulation);
                    m1 = GetParent(PubSize, ChromosomesPopulation);

                    //System.Diagnostics.Debug.WriteLine(" generation inside " + father.FitnessValue + " Mother is " + mother.FitnessValue);
                    int SFlag = 0;

                    while ((f1 == m1) & (SFlag < 10))
                    {
                        m1 = GetParent(PubSize, ChromosomesPopulation);

                        // mother = ChromosomesPopulation[m1];
                        SFlag++;

                    }


                    Tree child1 = new Tree();
                    Tree child2 = new Tree();
                   

                    merage(ChromosomesPopulation[f1], ChromosomesPopulation[m1], child1, child2); // generate the children 

                   
                    for (int q = 0; q < MutationRate; q++)
                    {
                        int GenNo1 = rand.Next(1, 99);
                        int Rsize1 = rand.Next(1, 14);
                        child1.nodes[GenNo1].Size = Rsize1;

                    }
                    for (int q = 0; q < MutationRate; q++)
                    {
                        int GenNo2 = rand.Next(1, 99);
                        int Rsize2 = rand.Next(1, 14);
                        child2.nodes[GenNo2].Size = Rsize2;


                    }


                    
                    child1.FitnessValue = child1.getFitness();
                    child2.FitnessValue = child2.getFitness();

                    if (child1.FitnessValue > child2.FitnessValue)
                    {

                        Tree newChild = new Tree();
                        newChild = child1;
                        Offspring.Insert(i,newChild);


                    }
                    else
                    {
                        Tree newChild = new Tree();
                        newChild = child2;
                        Offspring.Insert(i, newChild);

                    }

                } // endig of generation
                ChromosomesPopulation.Clear();
                for (int i = 0; i < PubSize; i++)
                {
                    Tree newTree1 = new Tree();
                    newTree1 = Offspring[i];
                    ChromosomesPopulation.Insert(i, newTree1);
                 //  System.Diagnostics.Debug.WriteLine(" the new pubulation is  " + i + "    " + ChromosomesPopulation[i].getFitness() + " Offspring " + Offspring[i].getFitness());

                }
                int fittestIndex = Offspring.FindIndex(tree => tree.FitnessValue == Offspring.Max(t => t.FitnessValue));
                
                BestMap = new Tree();
                BestMap = Offspring[fittestIndex];
                string message2 = "ListOfNodeResult= ";
                message3 += ", k=("+k+ ")=" + BestMap.FitnessValue.ToString();
                foreach (TreeNode node in BestMap.ListOfNodeResult)
                    message2 += " + " + node.Data.ToString();
              // if (k%15 == 0)
               //  MessageBox.Show(k+ message2);

                k++;
             
                row++;

                XtestFitness.writeXcelsheet(row, col, k.ToString());
               XtestFitness.writeXcelsheet(row, col + 1, BestMap.FitnessValue.ToString());
                //File.AppendAllText("log.txt", $"Processing step X at {DateTime.Now}{Environment.NewLine}");

            } while (k < Maxloop);
           
            bestGeneration = new Tree();
             bestGeneration = BestMap;
            System.Diagnostics.Debug.WriteLine("the final   " + bestGeneration.ListOfNodeResult.Count().ToString());

            string message1 = "the Final ListOfNodeResult= ";
            foreach (TreeNode node in BestMap.ListOfNodeResult)
                message1 += " + " + node.Data.ToString();
             MessageBox.Show(message1);

            MessageBox.Show(message3);

            System.Diagnostics.Debug.WriteLine(" the max fitness is after the max loop " + bestGeneration.FitnessValue);

           


        }

        int GetParent(int PubSize, List<Tree> ChromosomesPopulation)
        {
            int rP = r.Next(0, 100);

            if (rP > 50)

                return Tournament(PubSize, ChromosomesPopulation);
            else
                return Tournament(PubSize, ChromosomesPopulation); //Biased(PubSize, ChromosomesPopulation);



        }

        void merage(Tree p1, Tree p2, Tree ch1, Tree ch2)
        {

            TreeNode new_nodeP1 = new TreeNode { Data = new Point(p1.nodes[0].Data.X, p1.nodes[0].Data.Y), Size = p1.nodes[0].Size, Link = 0, Children = new List<TreeNode>(3), Door = 0 };
            TreeNode new_nodeP2 = new TreeNode { Data = new Point(p2.nodes[0].Data.X, p2.nodes[0].Data.Y), Size = p2.nodes[0].Size, Link = 0, Children = new List<TreeNode>(3), Door = 0 };
            ch1.Root = new_nodeP1;
            ch2.Root = new_nodeP2;
            ch1.nodes[0]=new_nodeP1;
            ch2.nodes[0]=new_nodeP2;
            for (int i = 1; i < 99; i++)
            {


                new_nodeP1 = new TreeNode { Data = new Point(p1.nodes[i].Data.X, p1.nodes[i].Data.Y), Size = p1.nodes[i].Size, Link = 0, Children = new List<TreeNode>(3), Door = 0 };
                new_nodeP2 = new TreeNode { Data = new Point(p2.nodes[i].Data.X, p2.nodes[i].Data.Y), Size = p2.nodes[i].Size, Link = 0, Children = new List<TreeNode>(3), Door = 0 };

                if (i<crossPoint)
                {
                    ch1.nodes[i]=new_nodeP1;
                    ch2.nodes[i]=new_nodeP2;
                }
                else
                {
                    ch1.nodes[i]=new_nodeP2;
                    ch2.nodes[i]=new_nodeP1;
                }

            }
            new_nodeP1 = new TreeNode { Data = new Point(p1.nodes[99].Data.X, p1.nodes[99].Data.Y), Size = p1.nodes[99].Size, Link = 0, Children = new List<TreeNode>(3), Door = 0 };
            new_nodeP2 = new TreeNode { Data = new Point(p2.nodes[99].Data.X, p2.nodes[99].Data.Y), Size = p2.nodes[99].Size, Link = 0, Children = new List<TreeNode>(3), Door = 0 };
           // System.Diagnostics.Debug.WriteLine(" ch1 count " + ch1.nodes.Count());
            ch1.nodes[99]= new_nodeP1;
            ch2.nodes[99]=new_nodeP2;
            ch1.CreatTreeNodes();
            ch2.CreatTreeNodes();
            
            ch1.FitnessValue = ch1.getFitness();
            ch2.FitnessValue = ch2.getFitness();
           // MessageBox.Show(" the count cild1 " + child1.ListOfNodeResult.Count().ToString());
            // System.Diagnostics.Debug.WriteLine(" number of nodes in result and fitness " + ch1.getFitness());

        }
        /*
        void merage1(Tree p1, Tree p2)
        {

           
            for (int i = 0; i < 100; i++)
            {


                int P1Size = p1.nodes[i].Size;
                int p2Size = p2.nodes[i].Size;
                if (i < 50)
                {
                    child1.nodes[i].Size = P1Size;
                    child2.nodes[i].Size = p2Size;
                }
                else
                {
                    child1.nodes[i].Size = p2Size;
                    child2.nodes[i].Size = P1Size;
                }

            }

            //child1.CreatTreeNodes();
            //child2.CreatTreeNodes();
            child1.FitnessValue = child1.getFitness();
            child2.FitnessValue = child2.getFitness();
            // MessageBox.Show(" the count cild1 " + child1.ListOfNodeResult.Count().ToString());
            // System.Diagnostics.Debug.WriteLine(" number of nodes in result and fitness " + ch1.getFitness());

        }
        */

        int Tournament(int PubSize, List<Tree> ChromosomesPopulation)
        {

           
            r1 = r.Next(0, PubSize - 1);
            r2 = r.Next(0, PubSize - 1);
            while (r1 == r2)
            {
                r2 = r.Next(0, PubSize - 1);
            }
           
           double tPare1 = ChromosomesPopulation[r1].FitnessValue;
            double tPare2 = ChromosomesPopulation[r2].FitnessValue;



            if (tPare1 > tPare2)
                return r1;
            else
                return r2;
        }


         int Biased(int PubSize, List<Tree> ChromosomesPopulation)
         {
            double totalFitness = ChromosomesPopulation.Sum(n => n.FitnessValue);
            List<Tree> SortChromosomesPopulation = ChromosomesPopulation.OrderByDescending(tree => tree.FitnessValue).ToList();
            
            var cumulativeProbabilities = new List<double>(ChromosomesPopulation.Count);
            double cumulativeTotal = 0.0;

            foreach (var individual in ChromosomesPopulation)
            {
                double proportion = individual.FitnessValue / totalFitness;
                cumulativeTotal += proportion;
                cumulativeProbabilities.Add(cumulativeTotal);
            }

            double selectiveValue = r.NextDouble();
            int selectedIndex = 0;

            for (int i = 0; i < cumulativeProbabilities.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine(" i= " + i + " cumulativeProbabilities[i] = " + cumulativeProbabilities[i] + " selectiveValue = " + selectiveValue);
                if (selectiveValue < cumulativeProbabilities[i])
                {
                    selectedIndex = i;
                    break;
                }
            }

            return selectedIndex;
        }

        void MutationClasic(ref Tree MuCh1, ref Tree MuCh2)
        {
            System.Random GenNo = new System.Random();
            System.Random Rsize = new Random();
            int[] GenNumbers1 = new int[MutationRate];
            int[] GenNumbers2 = new int[MutationRate];
            for (int i = 0; i < MutationRate; i++)
            {
                int GenNo1 = GenNo.Next(1, 100);
                 GenNumbers1[i] = GenNo1;

            }
                     
            int index = 0;
            foreach (int genNumber in GenNumbers1)
            {
                int Rsize1 = Rsize.Next(1, 14);

                MuCh1.nodes[genNumber].Size = Rsize1;
                index++;
            }

            //----------------------------------------------------the second child
            for (int i = 0; i < MutationRate; i++)
            {
                int GenNo1 = GenNo.Next(1, 100);
                 GenNumbers2[i] = GenNo1;

            }
           
            index = 0;
            foreach (int genNumber in GenNumbers2)
            {
                int Rsize1 = Rsize.Next(1, 14);

                MuCh2.nodes[genNumber].Size = Rsize1;
                index++;
            }
            MuCh1.FitnessValue=MuCh1.getFitness();
            MuCh2.FitnessValue = MuCh2.getFitness();

        }


        int getFitted(List<Tree> X, int PubSize) // return the best map in generation 

        {

            double fittedValue = 0;

            int fitted = 0;
            for (int i = 0; i < PubSize; i++)
            {
                if (X[i].FitnessValue > fittedValue)
                {
                    fittedValue = X[i].FitnessValue;
                    fitted = i;
                }

            }
            return fitted;
        }

        void DrawGraphInForm(PaintEventArgs e, int A, int step1, int step2, int ImgW, int ImgH, TreeNode[] BestNodes)
        {
            Room room1 = new Room();
            TreeNode root1 = new TreeNode();
           // System.Diagnostics.Debug.WriteLine(" size of bestnodes is " + BestNodes.Count());
            for (int i = 0; i < 100; i++)
            {
                root1 = new TreeNode();
                root1 = BestNodes[i];
                int r1 = root1.Size;


                switch (r1)
                {
                    case 1:

                        room1.NormalRoomSize1(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;

                    case 2:

                        room1.NormalRoomSize2(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;

                    case 3:
                        room1.NormalRoomSize3(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;

                    case 4:
                        room1.NormalRoomSize4(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;
                    case 5:
                        room1.NormalRoomSize5(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;
                    case 6:
                        room1.NormalRoomSize6(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;
                    case 7:
                        room1.NormalRoomSize7(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;

                    case 8:
                        room1.PolygonSize8(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;

                    case 9:

                        room1.PolygonSize9(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);
                        break;

                    case 10:

                        room1.PolygonSize10(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);
                        break;
                    case 11:

                        room1.HallWays11(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);
                        break;

                    case 12:

                        room1.HallWays12(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);
                        break;

                    case 13:

                        room1.EventRoomSize13(e, root1.Data.X * A + step1, root1.Data.Y * A + step2);

                        break;

                    default:
                        break;


                }
            }
            room1.ImagBorders(e, A + step1, A + step2, A * (ImgW), A * (ImgH));
        } // end of Dograph



    }
}
