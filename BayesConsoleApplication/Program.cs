using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smile;
using Smile.Learning;


namespace BayesConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            new Smile.License(
            "SMILE LICENSE 9aa6d3c3 c25a9935 1558c92f " +
            "THIS IS AN ACADEMIC LICENSE AND CAN BE USED  " +
            "SOLELY FOR ACADEMIC RESEARCH AND TEACHING, " +
            "AS DEFINED IN THE BAYESFUSION ACADEMIC  " +
            "SOFTWARE LICENSING AGREEMENT. " +
            "Serial #: bqc4bbh4fhqsd6cqb7lrwdrpm " +
            "Issued for: Patrycja Mundzik (p.mundzik@gmail.com) " +
            "Academic institution: Politechnika Bia\u0142ostocka " +
            "Valid until: 2018-06-15 " +
            "Issued by BayesFusion activation server",
            new byte[] {
                0x20,0xe7,0xfe,0x34,0xbc,0x07,0xc4,0x24,0x1e,0xad,0xe2,0xf7,0xec,0x33,0x66,0x60,
                0x9f,0xe3,0x0e,0x5e,0xbb,0x41,0xd0,0x26,0x10,0x26,0xfb,0xf2,0x52,0xa9,0xda,0x67,
                0x89,0x9d,0xfb,0xd7,0x21,0xb5,0x87,0x36,0x23,0x86,0xe0,0x12,0x6c,0xe4,0xa1,0x47,
                0x72,0x4c,0x95,0xb0,0xea,0xe1,0xbf,0xf8,0x2c,0xf3,0x1e,0x5d,0x92,0xa3,0x39,0x60
                }
            );

            try
            {
                Network net = new Network();
                //net.ReadFile(@"C:\Users\Patrycja\Documents\tutorial_a.xdsl");

                // ########
                net.ReadFile(@"C:\Users\Patrycja\Desktop\Praca inzynierska\nastroj-ksiazka-kategoria-okrojone-poprawione.xdsl");
                // ########
                foreach (int handle in net)
                Console.WriteLine("Node {0}, {1}, {2}", handle, net[handle], net.GetNodeName(handle));
                Console.WriteLine(net[5]); 

                net.UpdateBeliefs();

                //Console.WriteLine("Wpisz wartość noda");
                //string userinput = Console.ReadLine();

                //String[] aForecastOutcomeIds = net.GetOutcomeIds("Forecast");
                int outcomeIndex;
                //for (outcomeIndex = 0; outcomeIndex < aForecastOutcomeIds.Length; outcomeIndex++)
                //    if ("Moderate".Equals(aForecastOutcomeIds[outcomeIndex]))
                //        break;

                //double[] aValues = net.GetNodeValue("Forecast");
                //double P_ForecastIsModerate = aValues[outcomeIndex];

                //Console.WriteLine("P(\"Forecast\" = Moderate) = " + P_ForecastIsModerate);

                //// ###########
                //String[] aNastrojOutcomeIds = net.GetOutcomeIds("Nastroj");
                //String[] aJakaKsiazkaIds = net.GetOutcomeIds("JakaKsiazka");
                //int outcomeIndex1, outcomeIndex2;
                //for (outcomeIndex1 = 0; outcomeIndex1 < aNastrojOutcomeIds.Length; outcomeIndex1++)
                //{
                //    if("dobry".Equals(aNastrojOutcomeIds[outcomeIndex1]))
                //        for (outcomeIndex2 = 0; outcomeIndex2 < aJakaKsiazkaIds.Length; outcomeIndex2++)
                //        {
                //            if("wciagajaca")
                //        }
                //}

                // NASTROJ = DOBRY, KSIAZKA = WCIAGAJACA
                //net.SetEvidence("nastroj", "dobry");
                //net.UpdateBeliefs();

                //// Getting the index of the "Failure" outcome:
                //String[] aJakaKsiazkaOutcomeIds = net.GetOutcomeIds("JakaKsiazka");
                //for (outcomeIndex = 0; outcomeIndex < aJakaKsiazkaOutcomeIds.Length; outcomeIndex++)
                //    if ("wciagajaca".Equals(aJakaKsiazkaOutcomeIds[outcomeIndex]))
                //        break;

                //// Getting the value of the probability:
                //double[] aValues = net.GetNodeValue("JakaKsiazka");
                //double P_NastrojIsDobryJakaKsiazkaIsWciagajaca = aValues[outcomeIndex];

                //Console.WriteLine("P(\"Nastroj\" = dobry | \"JakaKsiazka\" = wciagajaca) = " + P_NastrojIsDobryJakaKsiazkaIsWciagajaca);
                


                net.ClearAllEvidence();

                // PYTAM O NASTROJ
                Console.WriteLine("Jaki masz nastrój?");
                string userinputNastroj = Console.ReadLine();

                // PYTAM O CECHĘ KSIĄŻKI
                Console.WriteLine("Jaką chciałbyś przeczytać książkę?");
                string userInputJakaKsiazka = Console.ReadLine();

                // USTAWIAM WARTOŚCI W SIECI NA PODSTAWIE ODPOWIEDZI UŻYTKOWNIKA
                net.SetEvidence("nastroj", userinputNastroj);
                net.SetEvidence(userInputJakaKsiazka, "tak");

                net.UpdateBeliefs();
                double P_poz_luz = 0, P_poz_wciag = 0, P_poz_luz_wciag = 0;
                double[] PValuesOfThirdLayer = { P_poz_luz, P_poz_wciag, P_poz_luz_wciag };
                double[] FindBiggestValue = new double[10];
                int i = 0;

                // --> po wybraniu przez użytkownika nastroju oraz pożądnych cech książki tutaj musze przejść 
                // po prawdopodopieństwach 3 warstwy
                for (int NodeId = 7; NodeId <= 16; NodeId++)
                {
                    String[] aForecastOutcomeIds = net.GetOutcomeIds(net[NodeId]);
                    int outcomeIndex1;
                    for (outcomeIndex1 = 0; outcomeIndex1 < aForecastOutcomeIds.Length; outcomeIndex1++)
                        if ("tak".Equals(aForecastOutcomeIds[outcomeIndex1]))
                            break;

                    double[] pValues = net.GetNodeValue(net[NodeId]);
                    double P_ChosenBook = pValues[outcomeIndex1];
                    
                    // DODAJĘ WARTOŚCI PRAWDOPODOBIEŃSTW DO TABLICY
                    FindBiggestValue[i] = P_ChosenBook;                  
                    i++;

                                                        
                    Console.WriteLine("P(Nastroj = {0} | JakaKsiazka = {1}) = | \"Gatunek\" {2}) = {3}", userinputNastroj, userInputJakaKsiazka, net[NodeId], P_ChosenBook);
                }

                // ZNAJDUJĘ WARTOŚĆ ORAZ POZYCJĘ NAJWIĘKSZEGO PRAWDOPODOBIEŃSTWA W TABLICY
                double maxNumber = FindBiggestValue.Max();
                int position = Array.IndexOf(FindBiggestValue, maxNumber);
                Console.WriteLine("max number: " + maxNumber + "index" + position);
                string ChosenBookCategory = net[position + 7];

                // WYŚWIETLAM NAZWĘ 
                Console.WriteLine("najbardziej prawdopodobna kategoria książki: " + ChosenBookCategory);

                if(ChosenBookCategory == "refWciag")
                {
                    Console.WriteLine("dobry wybór");
                }
                else {
                    Console.WriteLine("inna kategoria niz refWciag");
                }
               
                
                



                // przechodzę po wartościach odpowiedzi dla refWciag - wartości to tak lub nie
                String[] aGatunekOutcomeIds = net.GetOutcomeIds("refWciag");
                for (outcomeIndex = 0; outcomeIndex < aGatunekOutcomeIds.Length; outcomeIndex++)
                    if ("tak".Equals(aGatunekOutcomeIds[outcomeIndex]))
                        break;

                double[] aValues = net.GetNodeValue("refWciag");
                double P_NastrojIsDobryJakaKsiazkaIsWciagajacaGatunekIsRomans = aValues[outcomeIndex];

                //Console.WriteLine("P(\"Nastroj\" = {0} | \"JakaKsiazka\" = {1}) = | \"Gatunek\" = poz-luz) = {2}", userinputNastroj, userInputJakaKsiazka, P_NastrojIsDobryJakaKsiazkaIsWciagajacaGatunekIsRomans);
                


                //---------------------------------
                //net.SetEvidence("Forecast", "Good");
                //net.UpdateBeliefs();

                //// Getting the index of the "Failure" outcome:
                //String[] aSuccessOutcomeIds = net.GetOutcomeIds("Success");
                //for (outcomeIndex = 0; outcomeIndex < aSuccessOutcomeIds.Length; outcomeIndex++)
                //    if ("Failure".Equals(aSuccessOutcomeIds[outcomeIndex]))
                //        break;

                //// Getting the value of the probability:
                //aValues = net.GetNodeValue("Success");
                //double P_SuccIsFailGivenForeIsGood = aValues[outcomeIndex];

                //Console.WriteLine("P(\"Success\" = Failure | \"Forecast\" = Good) = " + P_SuccIsFailGivenForeIsGood);

                ////---------------------------------

                //net.ClearEvidence("Forecast");
                //net.SetEvidence("Forecast", "Poor");
                //net.UpdateBeliefs();

                //// Getting the index of the "Failure" outcome:
                //aSuccessOutcomeIds = net.GetOutcomeIds("Success");
                //for (outcomeIndex = 0; outcomeIndex < aSuccessOutcomeIds.Length; outcomeIndex++)
                //    if ("Failure".Equals(aSuccessOutcomeIds[outcomeIndex]))
                //        break;

                //// Getting the value of the probability:
                //aValues = net.GetNodeValue("Success");
                //double P_SuccIsSuccGivenForeIsPoor = aValues[outcomeIndex];

                //Console.WriteLine("P(\"Success\" = Success | \"Forecast\" = Poor) = " + P_SuccIsSuccGivenForeIsPoor);

                //double[] biggestNumber = { P_ForecastIsModerate, P_SuccIsFailGivenForeIsGood, P_SuccIsSuccGivenForeIsPoor };
                //double maxNumber = biggestNumber.Max();
                //Console.WriteLine(maxNumber);

                Console.ReadKey();

              


            }
            catch (SmileException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
