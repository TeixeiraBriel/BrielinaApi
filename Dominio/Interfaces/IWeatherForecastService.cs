﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Obtem();
    }
}
