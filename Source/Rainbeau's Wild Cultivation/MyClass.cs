using Harmony;
using RimWorld;
using System;
using System.Linq;
using Verse;

namespace RWC_Code {

	[StaticConstructorOnStartup]
	internal static class RWC_Initializer {
		static RWC_Initializer() {
			HarmonyInstance harmony = HarmonyInstance.Create("net.rainbeau.rimworld.mod.wildcultivation");
			harmony.Patch(AccessTools.Method(typeof(Designator_ZoneAdd_Growing), "CanDesignateCell"), new HarmonyMethod(typeof(Designator_ZoneAdd_Growing_RWC), "CanDesignateCellPrefix"), null);
			harmony.Patch(AccessTools.Method(typeof(Designator_ZoneAdd_Growing), "CanDesignateCell"), null, new HarmonyMethod(typeof(Designator_ZoneAdd_Growing_RWC), "CanDesignateCellPostfix"));
			LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
		}
		public static void Setup() {
			ThingDef.Named("PlantWildCotton").plant.harvestedThingDef = ThingDefOf.Plant_Cotton.plant.harvestedThingDef;
			ThingDef.Named("PlantWildCotton").plant.harvestYield = ThingDefOf.Plant_Cotton.plant.harvestYield;
			ThingDef.Named("PlantWildDevilstrand").plant.harvestedThingDef = ThingDefOf.Plant_Devilstrand.plant.harvestedThingDef;
			ThingDef.Named("PlantWildDevilstrand").plant.harvestYield = ThingDefOf.Plant_Devilstrand.plant.harvestYield;
		}
	}
	
	[DefOf]
	public static class ThingDefOf {
		public static ThingDef Plant_Cotton;
		public static ThingDef Plant_Devilstrand;
		public static ThingDef Plant_Potato;
	}

	public static class Designator_ZoneAdd_Growing_RWC {
		public static bool CanDesignateCellPrefix(ref float __state) {
			__state = ThingDefOf.Plant_Potato.plant.fertilityMin;
			ThingDefOf.Plant_Potato.plant.fertilityMin = 0.01f;
			return true;
		}
		public static void CanDesignateCellPostfix(ref float __state) {
			ThingDefOf.Plant_Potato.plant.fertilityMin = __state;
		}
	}
    
}
