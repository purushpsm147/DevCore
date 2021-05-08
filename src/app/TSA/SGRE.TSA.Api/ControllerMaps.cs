using System.Collections.Generic;

namespace SGRE.TSA.Api
{
    internal static class ControllerMaps
    {
        public record ControllerModule(string ResourceName, string ModuleName);
        public static IEnumerable<ControllerModule> GetControllerModuleMaps()
        {
            var toRet = new List<ControllerModule>();

            toRet.Add(new ControllerModule("Opportunities", "Create new opportunity"));
            toRet.Add(new ControllerModule("Opportunities", "Opporutinty information"));
            toRet.Add(new ControllerModule("Opportunities", "Opportunity key milestones"));
            toRet.Add(new ControllerModule("MileStones", "Opportunity key milestones"));

            //todo : Change Back to role once UI switches to /role Endpoint to update roles & Responsibilities
            toRet.Add(new ControllerModule("Opportunities", "Roles & Responsiblities"));

            toRet.Add(new ControllerModule("ProjectConstraints", "Project Constraints"));
            toRet.Add(new ControllerModule("ProjectConstraints", "Logistics constraints"));
            toRet.Add(new ControllerModule("ProjectConstraints", "Construction constraints"));

            toRet.Add(new ControllerModule("Proposals", "Scope & Responsibility"));
            toRet.Add(new ControllerModule("Proposals", "Certification"));

            toRet.Add(new ControllerModule("Quotes", "Quote"));
            toRet.Add(new ControllerModule("Quotes", "Add turbine type"));


            toRet.Add(new ControllerModule("Scenario", "Add baseline scenario"));
            toRet.Add(new ControllerModule("Scenario", "Add SST scenario"));
            toRet.Add(new ControllerModule("Scenario", "Tower Pre-design calculation/AEP P50 existing/proposed HH"));
            toRet.Add(new ControllerModule("Scenario", "Pre-design Dimensions"));
            toRet.Add(new ControllerModule("Scenario", "SST Design evaluation"));
            toRet.Add(new ControllerModule("Scenario", "Costs/Timeline/Feedback ESTIMATES STP/ETP(Baseline)"));
            toRet.Add(new ControllerModule("Scenario", "Costs/Timeline/Feedback ESTIMATES SST"));
            toRet.Add(new ControllerModule("Scenario", "Overview/Decisions"));
            toRet.Add(new ControllerModule("Scenario", "Costs/Timeline/Feedback  CALCULATION"));



            return toRet;
        }
    }
}