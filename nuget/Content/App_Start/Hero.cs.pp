using System;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof($rootnamespace$.App_Start.Hero), "PostStart")]

namespace $rootnamespace$.App_Start {
    public static class MySuperPackage {
        public static void PostStart() {
            // Add your start logic here
        }
    }
}