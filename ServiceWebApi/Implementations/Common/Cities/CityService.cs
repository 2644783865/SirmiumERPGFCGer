﻿using ApiExtension.Sender;
using ServiceInterfaces.Abstractions.Common.Cities;
using ServiceInterfaces.Messages.Common.Cities;
using ServiceInterfaces.ViewModels.Common.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWebApi.Implementations.Common.Cities
{
    public class CityService : ICityService
    {
            public CityListResponse GetCities()
            {
                CityListResponse response = new CityListResponse();
                try
                {
                    response = WpfApiHandler.GetFromApi<List<CityViewModel>, CityListResponse>("GetCities", new Dictionary<string, string>()
                {
                });
                }
                catch (Exception ex)
                {
                    response.Cities = new List<CityViewModel>();
                    response.Success = false;
                    response.Message = ex.Message;
                }

                return response;
            }

            public CityListResponse GetCitiesNewerThen(DateTime? lastUpdateTime)
            {
                CityListResponse response = new CityListResponse();
                try
                {
                    response = WpfApiHandler.GetFromApi<List<CityViewModel>, CityListResponse>("GetCitiesNewerThen", new Dictionary<string, string>()
                {
                    { "LastUpdateTime", lastUpdateTime.ToString() }
                });
                }
                catch (Exception ex)
                {
                response.Cities = new List<CityViewModel>();
                response.Success = false;
                response.Message = ex.Message;
            }

                return response;
            }

            public CityResponse Create(CityViewModel cityViewModel)
            {
                CityResponse response = new CityResponse();
                try
                {
                    response = WpfApiHandler.SendToApi<CityViewModel, CityResponse>(cityViewModel, "Create");
                }
                catch (Exception ex)
                {
                    response.City = new CityViewModel();
                    response.Success = false;
                    response.Message = ex.Message;
                }
                return response;
            }

            public CityResponse Delete(Guid identifier)
            {
                CityResponse response = new CityResponse();
                try
                {
                    CityViewModel viewModel = new CityViewModel();
                    viewModel.Identifier = identifier;
                    response = WpfApiHandler.SendToApi<CityViewModel, CityResponse>(viewModel, "Delete");
                }
                catch (Exception ex)
                {
                    response.City = new CityViewModel();
                    response.Success = false;
                    response.Message = ex.Message;
                }

                return response;
            }

            public CityListResponse Sync(SyncCityRequest request)
            {
                CityListResponse response = new CityListResponse();
                try
                {
                    response = WpfApiHandler.SendToApi<SyncCityRequest, CityViewModel, CityListResponse>(request, "Sync");
                }
                catch (Exception ex)
                {
                    response.Cities = new List<CityViewModel>();
                    response.Success = false;
                    response.Message = ex.Message;
                }

                return response;
            }
        }
    }
