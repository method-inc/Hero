using System;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof($rootnamespace$.App_Start.Hero), "Start")]

namespace $rootnamespace$.App_Start {
    public static class Hero {
        public static void Start() {
            // Add your start logic here
        }
    }
}