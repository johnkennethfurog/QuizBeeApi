using Prism.Commands;
using Prism.Mvvm;
using QuizBeeApp.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizBeeApp.Mobile.ViewModels
{
	public class PointsDisplayPageViewModel : BindableBase
	{
        public double Points => ParticipantHelper.points;
        public PointsDisplayPageViewModel()
        {

        }
	}
}
