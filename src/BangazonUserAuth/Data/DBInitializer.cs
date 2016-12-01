using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BangazonUserAuth.Models;

namespace BangazonUserAuth.Data
{
    //Class Name: DbInitializer
    //Author: Grant Regnier and Chris Smalley
    //Purpose of the class: The purpose of this class is to check if our database is empty, if it is, then this class populates the database with pre-made data.
    //Methods in Class: Initialize()
    public static class DbInitializer
    {
        //Method Name: Initialize
        //Purpose of the Method: This Method checks if the database has any data in it, if it doesn't it populates it with the data specified in the arrays bellow.
        //Arguments in Method: None.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BangazonUserAuthContext(serviceProvider.GetRequiredService<DbContextOptions<BangazonUserAuthContext>>()))
            {
              // Look for any products.
              if (context.Product.Any())
              {
                  return;   // DB has been seeded
              }

              var customers = new Customer[]
              {
                  new Customer { 
                      FirstName = "James",
                      LastName = "Regnier"
                  },
                  new Customer { 
                      FirstName = "Scott",
                      LastName = "Promises"
                  },
                  new Customer { 
                      FirstName = "Zaq",
                      LastName = "Repasse"
                  }
              };

              foreach (Customer c in customers)
              {
                  context.Customer.Add(c);
              }
              context.SaveChanges();

              var productTypes = new ProductType[]
              {
                  new ProductType { 
                      Name = "Electronics and Media"
                  },
                  new ProductType { 
                      Name = "Beauty, Health, and Grocery"
                  },
                  new ProductType { 
                      Name = "Appliances"
                  },
                  new ProductType { 
                      Name = "Everything Else"
                  },
              };

              foreach (ProductType i in productTypes)
              {
                  context.ProductType.Add(i);
              }
              context.SaveChanges();

              var subProductTypes = new SubProductType[]
              {
                  new SubProductType { 
                      Name = "Refrigerators and Freezers",
                      Description = "Appliances to keep food and beverages cold",
                      ProductTypeId = productTypes.Single(s => s.Name == "Appliances").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Microwaves",
                      Description = "Appliances to quickly heat food",
                      ProductTypeId = productTypes.Single(s => s.Name == "Appliances").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Ranges, Ovens, and Components",
                      Description = "Appliances to heat food",
                      ProductTypeId = productTypes.Single(s => s.Name == "Appliances").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Small Appliances",
                      Description = "A varity of small household appliances from coffee makers to irons to desk fans",
                      ProductTypeId = productTypes.Single(s => s.Name == "Appliances").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Washing Machine, Dryers, and Dishwashers",
                      Description = "Appliances to clean",
                      ProductTypeId = productTypes.Single(s => s.Name == "Appliances").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Portable Audio and Accessories",
                      Description = "MP3 players, headphones, wearable Accessories, portable bluetooth audio",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics and Media").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Computers, Hardware, Software, and Components",
                      Description = "Everything related to computers from printers, modems, Operating Systems, Microsoft Office, etc.",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics and Media").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Video Games and Gaming Hardware",
                      Description = "Gaming Consoles, Video Games, and Components",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics and Media").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Home Theatre and Audio",
                      Description = "Everything from TVs, Speakers, Bluray Players, and all the Components.",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics and Media").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Camera, Photo, and Video",
                      Description = "Everything related to photography and videography",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics and Media").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Media",
                      Description = "Digital Media, Books, DVDs, CDs, Records, Betatapes, Videotapes, Bluray. etc",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics and Media").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Car Electronics",
                      Description = "Car Audio, GPS, Etc.",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics and Media").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Food",
                      Description = "If you can eat it or drink it you will find it here.",
                      ProductTypeId = productTypes.Single(s => s.Name == "Beauty, Health, and Grocery").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Beauty",
                      Description = "Everything from fake eyelashes to makeup remover to cologne.",
                      ProductTypeId = productTypes.Single(s => s.Name == "Beauty, Health, and Grocery").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Health, Household, and Baby Care",
                      Description = "Everything from Tylenol to Detergent to Diapers to Mouse traps",
                      ProductTypeId = productTypes.Single(s => s.Name == "Beauty, Health, and Grocery").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Furniture",
                      Description = "From kitchen tables to sofas to mattresses",
                      ProductTypeId = productTypes.Single(s => s.Name == "Everything Else").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Clothing",
                      Description = "If you can wear it and its made of cloth, you will find here. Yeah we know technically you can wear almost anything but please keep it with stuff you might actually find at a clothing store. ",
                      ProductTypeId = productTypes.Single(s => s.Name == "Everything Else").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Misc",
                      Description = "If you can wear it and its made of cloth, you will find here. Yeah we know technically you can wear almost anything but please keep it with stuff you might actually find at a clothing store. ",
                      ProductTypeId = productTypes.Single(s => s.Name == "Everything Else").ProductTypeId
                  },
              };

              foreach (SubProductType i in subProductTypes)
              {
                  context.SubProductType.Add(i);
              }
              context.SaveChanges();

              var products = new Product[]
              {
                  new Product { 
                      Description = "Pampelmouse La Croix which taste real good",
                      SubProductTypeId = subProductTypes.Single(s => s.Name == "Food").SubProductTypeId,
                      Name = "Pampelmouse La Croix",
                      Price = 3.55,
                      CustomerId = 1
                  },
                  new Product { 
                      Description = "A 2012 iPod Shuffle. Headphones are included. 16G capacity.",
                      SubProductTypeId = subProductTypes.Single(s => s.Name == "Portable Audio and Accessories").SubProductTypeId,
                      Name = "iPod Shuffle",
                      Price = 18.00,
                      CustomerId = 1
                  },
                  new Product { 
                      Description = "Stainless steel refrigerator. Three years old. Minor scratches.",
                      SubProductTypeId = subProductTypes.Single(s => s.Name == "Refrigerators and Freezers").SubProductTypeId,
                      Name = "Samsung refrigerator",
                      Price = 500.00,
                      CustomerId = 1
                  }
              };

              foreach (Product i in products)
              {
                  context.Product.Add(i);
              }
              context.SaveChanges();
          }
       }
    }
}
