﻿using Models.Models;
using Models.ViewModels.Aquarium;

namespace Aqua_Sharp_Backend.Services.AquariumService
{
    public class AquariumService : IAquariumService
    {
        public Task<Aquarium> Add(CreateAquariumViewModel createAquariumViewModel)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Aquarium> Edit(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Aquarium>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Aquarium> GetOne(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
