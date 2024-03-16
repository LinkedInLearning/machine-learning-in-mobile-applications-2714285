using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Maui;
using System.Windows.Markup;

namespace MLSample.ViewModels
{
    public class HousingPredictionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public HousingPredictionViewModel()
        {
        }

        private double? _Crime = null;
        public double? Crime
        {
            get { return _Crime; }
            set
            {
                if (_Crime ==  null || _Crime != value)
                {
                    _Crime = value;
                    PropertyIsChanged(nameof(Crime));
                }
            }
        }

        private double? _ZoningPercent = null;
        public double? ZoningPercent
        {
            get { return _ZoningPercent; }
            set
            {
                if (_ZoningPercent ==  null || _ZoningPercent != value)
                {
                    _ZoningPercent = value;
                    PropertyIsChanged(nameof(ZoningPercent));
                }
            }
        }

        private double? _IndustryPercent = null;
        public double? IndustryPercent
        {
            get
            {
                return _IndustryPercent;
            }
            set
            {
                if (_IndustryPercent ==  null || _IndustryPercent != value)
                {
                    _IndustryPercent = value;
                    PropertyIsChanged(nameof(IndustryPercent));
                }
            }
        }

        private double? _RiverLot = null;
        public double? RiverLot
        {
            get
            {
                return _RiverLot;
            }
            set
            {
                if (_RiverLot ==  null || _RiverLot != value)
                {
                    _RiverLot = value;
                    PropertyIsChanged(nameof(RiverLot));
                }
            }
        }

        private double? _NoxConcentration = null;
        public double? NoxConcentration
        {
            get
            {
                return _NoxConcentration;
            }
            set
            {
                if (_NoxConcentration ==  null || _NoxConcentration != value)
                {
                    _NoxConcentration = value;
                    PropertyIsChanged(nameof(NoxConcentration));
                }
            }
        }

        private double? _Rooms = null;
        public double? Rooms
        {
            get
            {
                return _Rooms;
            }
            set
            {
                if (_Rooms ==  null || _Rooms != value)
                {
                    _Rooms = value;
                    PropertyIsChanged(nameof(Rooms));
                }
            }
        }

        private double? _HomeAge = null;
        public double? HomeAge
        {
            get
            {
                return _HomeAge;
            }
            set
            {
                if (_HomeAge ==  null || _HomeAge != value)
                {
                    _HomeAge = value;
                    PropertyIsChanged(nameof(HomeAge));
                }
            }
        }

        private double? _WorkDistance = null;
        public double? WorkDistance
        {
            get
            {
                return _WorkDistance;
            }
            set
            {
                if (_WorkDistance ==  null || _WorkDistance != value)
                {
                    _WorkDistance = value;
                    PropertyIsChanged(nameof(WorkDistance));
                }
            }
        }

        private double? _HighwayAccess = null;
        public double? HighwayAccess
        {
            get
            {
                return _HighwayAccess;
            }
            set
            {
                if (_HighwayAccess ==  null || _HighwayAccess != value)
                {
                    _HighwayAccess = value;
                    PropertyIsChanged(nameof(HighwayAccess));
                }
            }
        }

        private double? _TaxInThousands = null;
        public double? TaxInThousands
        {
            get
            {
                return _TaxInThousands;
            }
            set
            {
                if (_TaxInThousands ==  null || _TaxInThousands != value)
                {
                    _TaxInThousands = value;
                    PropertyIsChanged(nameof(TaxInThousands));
                }
            }
        }

        private double? _StudentTeacherRatio = null;
        public double? StudentTeacherRatio
        {
            get
            {
                return _StudentTeacherRatio;
            }
            set
            {
                if (_StudentTeacherRatio ==  null || _StudentTeacherRatio != value)
                {
                    _StudentTeacherRatio = value;
                    PropertyIsChanged(nameof(StudentTeacherRatio));
                }
            }
        }

        private double? _AfricanAmericanPercent = null;
        public double? AfricanAmericanPercent
        {
            get
            {
                return _AfricanAmericanPercent;
            }
            set
            {
                if (_AfricanAmericanPercent ==  null || _AfricanAmericanPercent != value)
                {
                    _AfricanAmericanPercent = value;
                    PropertyIsChanged(nameof(AfricanAmericanPercent));
                }
            }
        }

        private double? _PoorPercent = null;
        public double? PoorPercent
        {
            get
            {
                return _PoorPercent;
            }
            set
            {
                if (_PoorPercent ==  null || _PoorPercent != value)
                {
                    _PoorPercent = value;
                    PropertyIsChanged(nameof(PoorPercent));
                }
            }
        }

        private double _PredictedPrice = 0;
        public double PredictedPrice
        {
            get
            {
                return _PredictedPrice;
            }
            private set
            {
                if (_PredictedPrice != value)
                {
                    _PoorPercent = value;
                    PropertyIsChanged(nameof(PredictedPrice));
                }
            }
        }

        private Command _PredictHomePrice;
        public Command PredictHomePrice
        {
            get
            {
                if (_PredictHomePrice == null)
                {
                    _PredictHomePrice = new Command(async () => await PredictHousePriceAsync());
                }
                return _PredictHomePrice;
            }
        }



        private async Task PredictHousePriceAsync()
        {
            var predictedPrice = await GetHomePriceAsync(Crime, ZoningPercent, IndustryPercent, RiverLot, NoxConcentration, Rooms, HomeAge, WorkDistance, HighwayAccess, TaxInThousands, StudentTeacherRatio, AfricanAmericanPercent, PoorPercent); 

            PredictedPrice = predictedPrice;
        }

        private Task<double> GetHomePriceAsync(double? crime, double? zoningPercent, double? industryPercent, double? riverLot, double? noxConcentration, double? rooms, double? homeAge, double? workDistance, double? highwayAccess, double? taxInThousands, double? studentTeacherRation, double? africanAmericanPercent, double? poorPercent)
        {
            double returnValue = 0;

            var tcs = new TaskCompletionSource<double>();
            tcs.SetResult(returnValue);
            return tcs.Task;
        }

        private void PropertyIsChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}