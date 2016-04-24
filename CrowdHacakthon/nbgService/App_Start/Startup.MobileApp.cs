using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using nbgService.DataObjects;
using nbgService.Models;
using Owin;

namespace nbgService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);
            
            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new nbgInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<nbgContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();
            
            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class nbgInitializer : DropCreateDatabaseIfModelChanges<nbgContext>
    {
        
        protected override void Seed(nbgContext context)
        {

            List<Business> bus = new List<Business>
            {
                new Business {Id = "b0001",PayDay=18,LocationDetails="Αχαϊας 5, Παγκράτι",BackImage="http://www.kinfolk.com/wp-content/uploads/2014/02/Kinfolk_Web-Story_E5-Bakery-4.jpg",Employees = 5,Name="Ο Φούρνος του Δημήτρη",Type="Τρόφιμα",ProfileImage="http://gdj.gdj.netdna-cdn.com/wp-content/uploads/2014/11/psd-logo-10.jpg",Loan=15000,Paid=4000,Donation=2000,RoundUp=1967,Location="37.9591994,23.7576374",Details="Στο κατάστημα μας, ο πελάτης έχει να επιλέξει από μια πολύ μεγάλη ποικιλία φρέσκων προϊόντων ημέρας, σε εξαιρετικές τιμές. Με έμφαση στην άψογη εξυπηρέτηση, τα αγνά υλικά, και την ασύγκριτη φρεσκάδα, ο φούρνος μας προσφέρει μεγάλη γκάμα από Σάντουιτς, Κέικ, Τσουρέκια, Κουλούρια, Σφολιάτες, Κρουασάν, Λουκουμάδες, γλυκά, ειδικά αρτοσκευάσματα, γεύσεις άλλων λαών αλλά και παραδοσιακές γεύσεις του τόπου μας." },
                new Business {Id = "b0002",PayDay=1,LocationDetails="Δημοσθένους 2, Παγκράτι",BackImage="http://www.pdgm.com/getmedia/49986e29-2fba-4629-8a09-591205b91ed4/Library_1400_800.jpg.aspx",Employees= 15,Name="Φροντιστήριο Πυθαγορας",Type="Εκπαίδευση",ProfileImage="https://www.nhaschools.com/schools/preeminent/Style%20Library/NHASchools/images/nha-book-blue.png",Loan=6000,Paid=978,Donation=1654,RoundUp=867,Location ="37.9512579,23.7326903",Details="Οι εκπαιδευτικοί μας δεν είναι απλά εργαζόμενοι σε μια επιχείρηση. Έχουν αποκτήσει την φιλοσοφία και ενταχθεί στο σύστημα λειτουργίας του φροντιστηρίου ώστε να αποτελούν συνεργάτες. Οι πολλοί είναι μαζί μας πάνω από 4 έτη και άλλοι από τα λυκειακά τους χρόνια, αφού υπήρξαν μαθητές μας!" },
                new Business {Id = "b0003",PayDay=5,LocationDetails="Σύρου 24, Παγκράτι",BackImage="http://7-themes.com/data_images/out/23/6847738-cafe-wallpaper.jpg",Employees =14,Name="Το Νέον",Type="Καφετέρια",ProfileImage="https://s-media-cache-ak0.pinimg.com/736x/f8/9c/08/f89c08df04ea1d8257438035c9df2b0b.jpg",Loan=7500,Paid=3400,Donation=2012,RoundUp=1003,Location ="37.9993805,23.7377838",Details="Η παραγγελία του ωμού κόκκου από τις καλύτερες φάρμες σε όλο τον κόσμο, η δημιουργία χαρμανιών, η εύρεση της ιδανικής καμπύλης καβουρδίσματος, η τελετουργία της παρασκευής των ροφημάτων, οι συνεχείς έλεγχοι, οι μετρήσεις, ο σχεδιασμός και το στήσιμο του καταστήματος, η εκπαίδευση του προσωπικού των νέων και παλαιών καταστημάτων και η συνεχής υποστήριξή τους… όλα αντιμετωπίζονται με επιστημονικότητα και εμμονή στη λεπτομέρεια." },
                new Business {Id = "b0004",PayDay=8,LocationDetails="Ριζάρη 2, Παγκράτι", BackImage="http://iamsk.org/wp-content/uploads/2016/01/selling-used-baby-clothes.jpg",Employees=6,Name="Pull and Tiger",Type = "Κατάστημα Ρούχων",ProfileImage="http://blog.caplin.com/wp-content/uploads/logo.png",Loan=25000,Paid=8600,Donation=3002,RoundUp=2008,Location ="37.9754674,23.7435533",Details ="Είμαστε περήφανοι για την εξαιρετική εξυπηρέτηση των πελατών μας, καθώς και για τη μεγάλη μας ποικιλία σε παπούτσια, τσάντες και άλλα αξεσουάρ από διεθνούς φήμης brands." }
            };

            List<Item> itm = new List<Item>
            {
                new Item {BusinessId="b0001",Id = "i0001",Name ="Καρβελι Ψωμιού",Price=10,Image ="http://www.athensmagazine.gr/photos/articles/thumbs_large/7e25e5b2aba60cef50cec990c159638d.jpg" },
                new Item {BusinessId="b0001",Id = "i0002",Name ="Ζαμπονοτυροπιτα",Price=15,Image ="http://cookoo.gr/photos/large/zamponoturopita.jpg" },
                new Item {BusinessId="b0002",Id = "i0003",Name ="1ας Μήνας Μαθηματικά",Price=500,Image ="http://www.naftemporiki.gr/fu/p/658155/638/399/0x0000000000644b6d/2/mathimatika-mathimatikoi-tupoi-arxeiou.jpg" },
                new Item {BusinessId="b0002",Id = "i0004",Name ="1ας Μήνας Φυσική",Price=550,Image ="http://air.news.gr/cov/fy/fysiki_b2.jpg" },
                new Item {BusinessId="b0003",Id = "i0005",Name ="Καφές",Price=17,Image ="http://redcoffee.gr/wp-content/uploads/2015/10/o-kafes-den-apagorevete-stin-egimosini.jpg" },
                new Item {BusinessId="b0003",Id = "i0006",Name ="Ποτό",Price=47,Image ="http://www.4news.gr/wp-content/uploads/2014/12/4news.gr_864.jpg" },
                new Item {BusinessId="b0004",Id = "i0007",Name ="Μπλούζα",Price=150,Image ="http://images.mousoulis.gr/images/products/zoom/1366132091-91427700.jpg" },
                new Item {BusinessId="b0004",Id = "i0008",Name ="Παντελόνι",Price=200,Image ="http://www.zakcret.gr/eshop/image/data/zakcret/images/210206020001-20.jpg" },
            };

            List<Payment> pmt = new List<Payment>
            {
                new Payment {BusinessId="b0001",Id = "p0001",Type=1,Amount=100,Date= DateTime.Parse("14/02/2016 13:56")  },
                new Payment {BusinessId="b0001",Id = "p0002",Type=0,Amount=300,Date= DateTime.Parse("18/02/2016 14:02")  },
                new Payment {BusinessId="b0001",Id = "p0003",Type=0,Amount=300,Date= DateTime.Parse("18/03/2016 16:34")  },
                new Payment {BusinessId="b0001",Id = "p0004",Type=2,Amount=200,Date= DateTime.Parse("02/04/2016 18:07")  },
                new Payment {BusinessId="b0001",Id = "p0005",Type=1,Amount=400,Date= DateTime.Parse("09/04/2016 02:45")  },
                new Payment {BusinessId="b0001",Id = "p0006",Type=2,Amount=50,Date= DateTime.Parse("14/04/2016 19:23")  },
                new Payment {BusinessId="b0001",Id = "p0007",Type=1,Amount=40,Date= DateTime.Parse("17/04/2016 11:50")  },
                new Payment {BusinessId="b0001",Id = "p0008",Type=0,Amount=300,Date= DateTime.Parse("18/04/2016 13:00")  },

            };

            List<User> usr = new List<User>
            {
                new User {Id="u0001",CardNumber="0000-0000-0000-0000",Points=11130,Name="Γιώργος",Surname="Γεωργίου",Balance=1205,ProfileImage="https://aegean-lab.tk/NGB/Users-01.png" },
                new User {Id="u0002",CardNumber="1111-1111-1111-1111",Points=2746,Name="Ελένη",Surname="Παπαδοπούλου",Balance=957,ProfileImage="https://aegean-lab.tk/NGB/Users-02.png" },
                new User {Id="u0003",CardNumber="2222-2222-2222-2222",Points=6972,Name="Νίκος",Surname="Νίκος",Balance=6704,ProfileImage="https://aegean-lab.tk/NGB/Users-03.png" },
                new User {Id="u0004",CardNumber="3333-3333-3333-3333",Points=361,Name="Αλέξανδρος",Surname="Αλεξάνδρου",Balance=59,ProfileImage="https://aegean-lab.tk/NGB/Users-04.png" },
            };

            foreach (var b in bus)
            {
                context.Businesses.Add(b);
            }
            foreach (var b in itm)
            {
                context.Items.Add(b);

            }
            foreach (var b in usr)
            {
                context.Users.Add(b);

            }
            foreach (var b in pmt)
            {
                context.Payments.Add(b);

            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}

