using PhotoBoom.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace PhotoBoom.Repository
{
    public class PhotoRepository
    {
        PhotoBoomDBEntities _photoDb = new PhotoBoomDBEntities();


        public List<Image> ListAllImages() {
           return _photoDb.Images.ToList();
        }

        public Image GetImage(int id) {
            return _photoDb.Images.FirstOrDefault(x => x.ImageId == id);
        }

        public void DeleteImage(int id)
        {
            Image originalImage = _photoDb.Images.FirstOrDefault(x => x.ImageId == id);

            _photoDb.Images.Remove(originalImage);
            _photoDb.SaveChanges();
        }

        public void Add(Image image)
        {
            try
            {

                _photoDb.Images.Add(image);
                _photoDb.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}