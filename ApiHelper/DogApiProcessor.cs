using ApiHelper.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class DogApiProcessor
    {
        public static async Task<DogBreedsModel> LoadBreedList()
        {
            ///TODO : À compléter LoadBreedList
            /// Attention le type de retour n'est pas nécessairement bon
            /// J'ai mis quelque chose pour avoir une base
            /// TODO : Compléter le modèle manquant
            /// 

            string url;

            url = $"https://dog.ceo/api/breeds/list/all";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogBreedsModel dogBreeds = await response.Content.ReadAsAsync<DogBreedsModel>();
                    return dogBreeds;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<DogPicturesModel> GetImageUrl(string breed, string nbrImg)
        {
            /// TODO : GetImageUrl()
            /// TODO : Compléter le modèle manquant
            string url;
            if (breed != "ALL")
                url = $"https://dog.ceo/api/breed/{breed}/images/random/{nbrImg}";
            else
                url = $"https://dog.ceo/api/breeds/image/random/{nbrImg}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogPicturesModel dogPictures = await response.Content.ReadAsAsync<DogPicturesModel>();
                    return dogPictures;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
