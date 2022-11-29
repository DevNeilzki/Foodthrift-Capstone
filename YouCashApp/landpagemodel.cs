using System;
using System.Collections.Generic;
using System.Text;

namespace YouCashApp
{
    public class landpagemodel
    {
        public landpagemodel()
        {
          Onboardings = GetOnboarding();
        }

    public List<Onboarding> Onboardings { get; set; }


    private List<Onboarding> GetOnboarding()
    {
        return new List<Onboarding>
        {
            new Onboarding { Heading = "Donate to our Fellow in a Minutes", Caption = "From small community pantry to a large community drives." },
            new Onboarding { Heading = "Browse Organization in one App", Caption = "View the top organizations whos top donors." },
            new Onboarding { Heading = "Explore Charities Everywhere", Caption = "Select preffered charity or organization to donate." },
        };
    }

    }
    public class Onboarding
    {
    public string Heading { get; set; }
    public string Caption { get; set; }
    }
}
