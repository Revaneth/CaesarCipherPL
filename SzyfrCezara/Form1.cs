using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


namespace SzyfrCezara
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            IUpDown();
        }

        private int Wprzes = 90;
        private string Wczytane = "";
    
        private char[] Przerobione ;
    

        private char[] alfabet = new char[78] {   'a','ą', 'b', 'c','ć',         
                                                  'd','e', 'ę', 'f','g',
                                                  'h','i', 'j', 'k','l',
         
                                                  'ł','m', 'n', 'ń','o',

                                                  'ó','p', 'q', 'r','s',         
                                                  'ś','t', 'u', 'v','w',
                                                  'x','y', 'z', 'ź','ż',   

        ' ','!','\"','#','$',   '%','&','\'','(',')',   '*','+',',','-','.',    
        '/','0','1','2','3',    '4','5','6','7','8',    '9',':',';','<','=',
        '>','?','@','[','\\',   ']','^','_','`','{',    '|','}','~'};//,(char)10, (char)13};        



        


        public void IUpDown()
        {

        numericUpDownS.Maximum = numericUpDownD.Maximum = alfabet.Count();
            numericUpDownD.Minimum=  numericUpDownS.Minimum = 0;
            numericUpDownD.Value= numericUpDownS.Value =   0;

                   
       }

 //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~cz gł ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        private void button_losuj_Click_1(object sender, EventArgs e)
        {
            int losowa;
            Random rand = new Random();
            losowa = rand.Next(0, 78);
            this.Wprzes = losowa;
            numericUpDownS.Value =Wprzes;
          //  msbx(1, this.Wprzes);
        }

        private string  load() {
        
                  
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Text files|*.txt";
            openFileDialog1.Title = "Wybierz plik";
            string plik="";
           

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                try
                {

                    plik = File.ReadAllText(openFileDialog1.FileName);
                 

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Nie moge wczytać pliku! \n" + ex.Message + ex.StackTrace);

                }

            }//if


            openFileDialog1.Dispose();


            return plik;
           
        
        }

        private void save(string s) {

           
            SaveFileDialog savefiledialog1 = new SaveFileDialog();
            savefiledialog1.Filter = "Text files|*.txt";
            savefiledialog1.Title = " Zapisz wynik ";
          //  savefiledialog1.AddExtension = true;
            
            
            savefiledialog1.CreatePrompt = true;
            savefiledialog1.OverwritePrompt = true;

            if ( savefiledialog1.ShowDialog() == DialogResult.OK)
            {   
     
              try{

               // using (StreamWriter sw = new StreamWriter(savefiledialog1.FileName)){
                
                  
                
            //    sw.Write(s);

              File.WriteAllText(savefiledialog1.FileName+"_"+ Wprzes+".txt", s);
                
              //  }

            }catch (Exception ex)
                {
                    MessageBox.Show("Error: Nie moge zapisać pliku! \n" + ex.Message + ex.StackTrace);
                }
        }//if



            savefiledialog1.Dispose();





        }


        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
        string zmiana = Wczytane;
            string nowy =  load();
            nowy =nowy.ToLowerInvariant();

            //foreach (string s in g)
            //{
            //    nowy += s;
            //}
            if (nowy!=null)
                {
                Wczytane = nowy;
                MessageBox.Show(nowy);

                nowy.ToLowerInvariant();

                this.Przerobione = nowy.ToCharArray();

                czysc();

                }





        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(Wczytane);//+">\n -- \n --- \n --- \n -- \n<" +Wprzes);
        }


 


//----------------------buttons---------------------------------


        private void okS_Click(object sender, EventArgs e)//szyfru
        {
        if ((Przerobione != null) && (Wczytane != null))
            {


            int pom = 99;
            char[] pomc = new char[Przerobione.Count()];
            string s = "";

            if (Wprzes <= alfabet.Count() && Wprzes >= 0)
                {

                if (Przerobione.Count() != 0)
                    {


                    for (int c = 0; c < Przerobione.Count(); c++)
                        {

                        pom = (szukaj(Przerobione[c]) + Wprzes) % alfabet.Count();
                        // pomc[c] = (char)pom[c];
                        if (pom > alfabet.Count() - 1)
                            pom = pom - alfabet.Count();
                        pomc[c] = alfabet[pom];
                        }




                    }


                }


            else { msbx(99, 0); }

            if (Wprzes <= alfabet.Count() && Wprzes >= 0)
                {
                foreach (char ch in pomc)
                { s += ch; }
                MessageBox.Show(s);
                txtuz.Text = Wczytane = textBox2.Text = s;
                }




        

            }
        else { msbx(7, 0); }
        }

        private void okD_Click(object sender, EventArgs e)//deszyfruj
            {
            if ((Przerobione != null) && (Wczytane != null))
                {
                int pom = 99;
                char[] pomc = new char[Przerobione.Count()];
                string s = "";

                if (Wprzes <= alfabet.Count() && Wprzes >= 0)
                    {

                    if (Przerobione.Count() != 0)
                        {


                        for (int c = 0; c < Przerobione.Count(); c++)
                            {

                            pom = (szukaj(Przerobione[c]) - Wprzes) % alfabet.Count();
                            // pomc[c] = (char)pom[c];
                            if (pom < 0)
                                pom = pom + alfabet.Count();

                            pomc[c] = alfabet[pom];
                            }




                        }


                    }


                else { msbx(99, 0); }


                if (Wprzes <= alfabet.Count() && Wprzes >= 0)
                    {
                    foreach (char ch in pomc)
                    { s += ch; }
                    MessageBox.Show(s);
                    textBox2.Text = Wczytane = txtuz.Text = s;
                    }




             

                //
                }
            else msbx(7, 1);
            }


        private void czysc()
            {
            string con = "";
            int h = 0,d=0;
            for (int c = 0; c < Przerobione.Count(); c++)
            {
            h = 0;
            d = 0;

            while (h < alfabet.Count())
                {

                for (int alf = 0; alf < alfabet.Count(); alf++)
                    {


                    if (Przerobione[c] != alfabet[alf])
                        {
                        h++;
                        }
                    else
                    {
                    d = 1;
                   // h = 0;
                    break;
                        }
                   
                        }

                    if (d == 1) break;


                    }
                Debug.WriteLine("h - " +h);
                if (h >= alfabet.Count())
                    {
                   // Debug.WriteLine(" nieznany znak" + Przerobione[c]);
                    Przerobione[c] = '#';
                   Debug.WriteLine(" nieznany znak" + Przerobione[c]);
                 //  con += Przerobione[c];
                  // d= h = 0;
                    }
               // else { h= d=0;}

                con += Przerobione[c];
                             
              }//for c

            Wczytane = textBox1.Text = con;


                }

        private int szukaj(char cha) {
        int  j= 90;
        for (int i = 0; i < alfabet.Count(); i++)
            {

            if(alfabet[i]==cha){
            j = i;
                
                }

            }
        if (j == 90) { j = 0; 

                MessageBox.Show("nie ma takiego znaku!!");
                } 
            return j;
            }
       
        
       //===========================msbxx===================================


        private void msbx(int sw, int wart) { 
        switch (sw){
            case 1: MessageBox.Show("Wartosc przesuniecia wynosi " + wart," wartosc przesuniecia" );
                break;

            case 2:
                MessageBox.Show("    Program szyfruje podany tekst metodą \"Szyfru Cezara\" "+
                    "(zwany też szyfrem przesuwającym, kodem Cezara lub przesunięciem Cezariańskim) "+
                    "w kryptografii jedna z najprostszych technik szyfrowania. "+
              " Jest to rodzaj szyfru podstawieniowego, w którym każda litera tekstu "+
               "jawnego (niezaszyfrowanego) zastępowana jest oddaloną od niej o "+
               "stałą liczbę pozycji w alfabecie inną literą (szyfr monoalfabetyczny), "+
               "przy czym kierunek zamiany musi być zachowany. Nie rozróżnia się przy "+
               "tym liter dużych i małych. Nazwa szyfru pochodzi od Juliusza Cezara, " +
                " który prawdopodobie używał tej techniki do komunikacji ze swymi przyjaciółmi.");
                msbx(4, 0);
                break;

            case 98350 :
                MessageBox.Show(@"  Alicja Bakoś
    nr indeksu 98350
    gr. I1-2"      , "O Autorce");

                break;

            case 66:
                MessageBox.Show("  Cath ^^", "O Autorce");

                break;



            case 3:
                MessageBox.Show("Najpierw nalezy wybrać przesunięcie, a następnie wczytać lub wpisac tekst w odpowiednie pole , po pierwszej operacji wynik pozostaje w pamieci do następnej modyfikacji- szyfrowania lub deszyfrowania - stan pamięci mozna sprawdzic w zakładce 'Sprawdź dane'\n nieznane znaki zastąpione sa przez znak \' # \'");
                break;

            case 4:
                string s= ""+alfabet[0];
                //foreach (char ch in alfabet)

                for (int i = 1; i < alfabet.Count() - 1; i++)
                { s = s + "," + alfabet[i]; }
                s += ","+alfabet[alfabet.Count()-1];

                MessageBox.Show("Program korzysta z alfabetu w postaci: \n"+ 
                    "A= { "+    s + " }"
                    + "\n\tbez rozróżnienia wielkości liter");

                break;


            case 7 :
                MessageBox.Show("Podaj tekst ");

                        break;


            case 99 :
                MessageBox.Show("Podaj wartość przesunięcia");

                break;


            default :
                MessageBox.Show("   SzyfrCezara C#  6 października 2012 ");
                break;
        }
        }

//---------------------------ni---------------------------------------------------------

        
       private void oAutorceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
         //   msbx(66, 0);
        msbx(98350,0);

        }

       private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msbx(2, 0);
        }

      
      //..............................
       private void numericUpDownS_ValueChanged_1(object sender, EventArgs e)
       {
           Wprzes = (int)numericUpDownS.Value;
           
           numericUpDownD.Value = Wprzes;
           textBox3.Text = "" + Wprzes;
       }

       private void numericUpDownD_ValueChanged(object sender, EventArgs e)
       {
         Wprzes= (int) numericUpDownD.Value;
        
         numericUpDownS.Value = Wprzes;
         textBox3.Text = "" + Wprzes;
      //   msbx(1, Wprzes);
       }

       private void resetToolStripMenuItem_Click(object sender, EventArgs e)
       {
          // char[] n = new char[1];
         //  Przerobione = n;
           Wczytane = "";
           Wprzes = 90;      
           numericUpDownD.Value= numericUpDownS.Value = 0;
           textBox3.Text = "" + Wprzes;
           textBox1.Text = Wczytane;

       }



       private void txtuz_TextChanged_1(object sender, EventArgs e)//szyf
       {
           Wczytane = txtuz.Text.ToLowerInvariant();
          // textBox2.Text =  
               textBox1.Text = Wczytane;
           this.Przerobione = Wczytane.ToCharArray();
        
           czysc();
        //   textBox2.Text = Wczytane;
       }

       private void textBox2_TextChanged(object sender, EventArgs e) //desz
       {

           Wczytane = textBox2.Text.ToLowerInvariant();
          // txtuz.Text= 
               textBox1.Text = Wczytane;
           this.Przerobione = Wczytane.ToCharArray();
         
           czysc();

         //  txtuz.Text = Wczytane;
       }

       private void pochodzenieSzyfruToolStripMenuItem_Click(object sender, EventArgs e)
           {
           msbx(3, 0);
           }

     



       
    }
}
