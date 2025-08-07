using System;

namespace STRINGS
{
	// Token: 0x02000F9F RID: 3999
	public class CODEX
	{
		// Token: 0x0200219A RID: 8602
		public class CRITTERSTATUS
		{
			// Token: 0x04009A5A RID: 39514
			public static LocString CRITTERSTATUS_TITLE = "Field Guide";

			// Token: 0x02002B6A RID: 11114
			public class METABOLISM
			{
				// Token: 0x0400BE3F RID: 48703
				public static LocString TITLE = "Metabolism";

				// Token: 0x020038C0 RID: 14528
				public class BODY
				{
					// Token: 0x0400E4B1 RID: 58545
					public static LocString CONTAINER1 = "A critter's metabolic rate is a measure of their appetite and the materials that they excrete as a result.\n\nCritters with higher metabolism get hungry more often. Those with lower metabolism will consume less food, but this reduced caloric intake results in fewer resources being produced.\n\nThe digestive process is influenced by conditions such as domestication, mood, and whether the critter in question is a juvenile (baby) or an adult.";
				}

				// Token: 0x020038C1 RID: 14529
				public class HUNGRY
				{
					// Token: 0x0400E4B2 RID: 58546
					public static LocString TITLE = "Hungry";

					// Token: 0x0400E4B3 RID: 58547
					public static LocString CONTAINER1 = "Tame critters have significantly faster metabolism than wild ones, and get hungry sooner. This makes them more valuable in terms of resource production, as long as the colony is equipped to meet their dietary needs.\n\nCritters' stomachs vary in size, but they are capable of storing at least five cycles' worth of food. Their bellies begin to rumble when those internal caches drop below 90 percent. The critter will then seek out food, and will continue to eat until they feel completely full again.\n\nJuvenile critters have the slowest metabolism, although glum tame critters are not far behind.";
				}

				// Token: 0x020038C2 RID: 14530
				public class STARVING
				{
					// Token: 0x0400E4B4 RID: 58548
					public static LocString TITLE = "Starving";

					// Token: 0x0400E4B5 RID: 58549
					public static LocString CONTAINER1_VANILLA = "With the exception of Morbs—which require zero calories to survive—tame critters will die after {0} cycles of consistent starvation. Wild critters do not starve to death.";

					// Token: 0x0400E4B6 RID: 58550
					public static LocString CONTAINER1_DLC1 = "With the exception of Morbs and Beetas—which require zero calories to survive—tame critters will die after {0} cycles of consistent starvation. Wild critters do not starve to death.";
				}
			}

			// Token: 0x02002B6B RID: 11115
			public class MOOD
			{
				// Token: 0x0400BE40 RID: 48704
				public static LocString TITLE = "Mood";

				// Token: 0x020038C3 RID: 14531
				public class BODY
				{
					// Token: 0x0400E4B7 RID: 58551
					public static LocString CONTAINER1 = "As with many living things, critters are susceptible to fluctuations in mood. While they are incapable of articulating their feelings verbally, these variations have observable effects on productivity and reproduction.\n\nFactors that influence a critter's mood include: grooming, wildness/tameness, habitat, overcrowding, confinement, and Brackene consumption.";
				}

				// Token: 0x020038C4 RID: 14532
				public class HAPPY
				{
					// Token: 0x0400E4B8 RID: 58552
					public static LocString TITLE = "Happy";

					// Token: 0x0400E4B9 RID: 58553
					public static LocString CONTAINER1 = "Happy, tame critters produce more usable materials and tend to lay eggs at a higher rate than glum or wild critters. Domesticated critters are less resilient than wild ones—they require more care from the colony in order to maintain a positive disposition.\n\nBabies have a higher baseline of natural joy, but produce neither resources nor eggs.\n\nDuplicants with the Critter Ranching skill have the expertise needed to domesticate and care for critters. They can boost a critter's mood and tend to their health at a Grooming Station.\n\nCritters who drink at the Critter Fountain also enjoy a mood boost, despite the lack of nutrients available in the Brackene dispensed.\n\nBeing confined or feeling crowded undermines a critter's happiness.";

					// Token: 0x0400E4BA RID: 58554
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4BB RID: 58555
					public static LocString HAPPY_METABOLISM = "    • Indirectly improves egg-laying rates";
				}

				// Token: 0x020038C5 RID: 14533
				public class NEUTRAL
				{
					// Token: 0x0400E4BC RID: 58556
					public static LocString TITLE = "Satisfied";

					// Token: 0x0400E4BD RID: 58557
					public static LocString CONTAINER1 = "When a critter has no reason to object to anything in its environment or diet, it will feel quite content with its lot in life. Satisfied critters have the default metabolism, fertility and life span expected of their species.";
				}

				// Token: 0x020038C6 RID: 14534
				public class GLUM
				{
					// Token: 0x0400E4BE RID: 58558
					public static LocString TITLE = "Glum";

					// Token: 0x0400E4BF RID: 58559
					public static LocString CONTAINER1 = "Critters can survive in subpar environments, but it takes a toll on their mood and impacts metabolism and productivity. When their happiness levels dip below zero, they become glum.\n\nWild critters are less sensitive to the effects of glumness than their tamed brethren, though they are still negatively affected by crowded or confined living conditions.";

					// Token: 0x0400E4C0 RID: 58560
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4C1 RID: 58561
					public static LocString GLUMWILD_METABOLISM = "    • Critter Metabolism\n";
				}

				// Token: 0x020038C7 RID: 14535
				public class MISERABLE
				{
					// Token: 0x0400E4C2 RID: 58562
					public static LocString TITLE = "Miserable";

					// Token: 0x0400E4C3 RID: 58563
					public static LocString CONTAINER1 = "When too many unpleasant conditions add up, critters become utterly miserable. This level of unhappiness seriously undermines their ability to contribute to the colony. Miserable critters have lower metabolism and will not lay eggs.";

					// Token: 0x0400E4C4 RID: 58564
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4C5 RID: 58565
					public static LocString MISERABLEWILD_METABOLISM = "    • Critter Metabolism";

					// Token: 0x0400E4C6 RID: 58566
					public static LocString MISERABLEWILD_FERTILITY = "    • Reproduction";
				}

				// Token: 0x020038C8 RID: 14536
				public class HOSTILE
				{
					// Token: 0x0400E4C7 RID: 58567
					public static LocString TITLE = "Hostile";

					// Token: 0x0400E4C8 RID: 58568
					public static LocString CONTAINER1_VANILLA = "Most critters are non-hostile. They may attempt to defend themselves when attacked by Duplicants, though their natural passivity limits the damage caused in these instances.\n\nSome critters, however, have exceptionally strong self-preservation instincts and must be approached with extreme caution.\n\nPokeshells, for example, are not naturally hostile but are fiercely protective of their young and will attack if a Duplicant or critter wanders too close to their eggs.";

					// Token: 0x0400E4C9 RID: 58569
					public static LocString CONTAINER1_DLC1 = "Most critters are non-hostile. They may attempt to defend themselves when attacked by Duplicants, though their natural passivity limits the damage caused in these instances.\n\nSome critters, however, have exceptionally strong self-preservation instincts and must be approached with extreme caution. Pokeshells, for example, are not naturally hostile but are fiercely protective of their young and will attack if a Duplicant or critter wanders too close to their eggs.\n\nThe Beeta, on the other hand, is both hostile and radioactive. While it cannot be tamed, it can be subdued through the use of CO2.";
				}

				// Token: 0x020038C9 RID: 14537
				public class CONFINED
				{
					// Token: 0x0400E4CA RID: 58570
					public static LocString TITLE = "Confined";

					// Token: 0x0400E4CB RID: 58571
					public static LocString CONTAINER1 = "Each species has its own space requirements. Critters who find themselves in a room that they consider too small will feel confined. They will feel the same way if they become stuck in a door or tile. Critters will not reproduce while they are in this state.\n\nShove Voles are the exception to this rule: their tunneling instincts make them quite comfortable in snug spaces, and they never feel confined.";

					// Token: 0x0400E4CC RID: 58572
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4CD RID: 58573
					public static LocString CONFINED_FERTILITY = "    • Reproduction\n";

					// Token: 0x0400E4CE RID: 58574
					public static LocString CONFINED_HAPPINESS = "    • Happiness";
				}

				// Token: 0x020038CA RID: 14538
				public class OVERCROWDED
				{
					// Token: 0x0400E4CF RID: 58575
					public static LocString TITLE = "Crowded";

					// Token: 0x0400E4D0 RID: 58576
					public static LocString CONTAINER1 = "This occurs when a critter is in a room that's appropriately sized for its needs but feels that there are too many other critters sharing the same space. Because each species has its own space requirements, this state can vary among occupants of the same room.\n\nThis emotional state intensifies in response to the number of excess critters: adding new critters to an already crowded room will undermine a critter's happiness even further.";

					// Token: 0x0400E4D1 RID: 58577
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4D2 RID: 58578
					public static LocString OVERCROWDED_HAPPY1 = "    • Happiness\n";
				}
			}

			// Token: 0x02002B6C RID: 11116
			public class FERTILITY
			{
				// Token: 0x0400BE41 RID: 48705
				public static LocString TITLE = "Reproduction";

				// Token: 0x020038CB RID: 14539
				public class BODY
				{
					// Token: 0x0400E4D3 RID: 58579
					public static LocString CONTAINER1 = "Reproductive rates and methods vary among species. The majority lay eggs that must be incubated in order to hatch the next generation of critters.\n\nFactors that influence the rate of reproduction include egg care, happiness, living conditions and domestication.";
				}

				// Token: 0x020038CC RID: 14540
				public class FERTILITYRATE
				{
					// Token: 0x0400E4D4 RID: 58580
					public static LocString TITLE = "Reproduction Rate";

					// Token: 0x0400E4D5 RID: 58581
					public static LocString CONTAINER1 = "Each time a critter completes their reproduction cycle (i.e. at 100 percent), it lays an egg and restarts its cycle.\n\nA critter's environment greatly impacts its base reproduction rate. When a critter is feeling cramped, it will wait until all eggs in the room have hatched or been removed before laying any of its own.\n\nCritters will also stop reproducing when they feel confined, which happens when their space is too small or they are stuck in a door or tile.\n\nMood and domestication also impact reproduction: happy critters reproduce more regularly, and happy tame critters reproduce the fastest.";
				}

				// Token: 0x020038CD RID: 14541
				public class EGGCHANCES
				{
					// Token: 0x0400E4D6 RID: 58582
					public static LocString TITLE = "Egg Chances";

					// Token: 0x0400E4D7 RID: 58583
					public static LocString CONTAINER1 = "In most cases, an egg will hatch into the same critter variant as its parent. Genetic volatility, however, means that there is a chance that it may hatch into another variant from that species.\n\nThere are many things that can alter the likelihood of a critter laying a particular type of egg.\n\nEgg chances are impacted by:\n    • Diet\n    • Body temperature\n    • Ambient gasses and elements\n    • Plants in the critters' care\n    • Variants that share the enclosure\n\nWhen a tame critter lays an egg, the resulting offspring will be born tame.";
				}

				// Token: 0x020038CE RID: 14542
				public class FUTURE_OVERCROWDED
				{
					// Token: 0x0400E4D8 RID: 58584
					public static LocString TITLE = "Cramped";

					// Token: 0x0400E4D9 RID: 58585
					public static LocString CONTAINER1 = "Crowded critters—or critters who know they'll start feeling crowded once all of the eggs in the room have hatched—will temporarily stop laying eggs. Their reproductive system will resume function once all eggs have hatched or been removed from the room.";

					// Token: 0x0400E4DA RID: 58586
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4DB RID: 58587
					public static LocString CRAMPED_FERTILITY = "    • Reproduction";
				}

				// Token: 0x020038CF RID: 14543
				public class INCUBATION
				{
					// Token: 0x0400E4DC RID: 58588
					public static LocString TITLE = "Incubation";

					// Token: 0x0400E4DD RID: 58589
					public static LocString CONTAINER1 = "A critter's incubation time is one-fifth of their total lifetime: for example, if a critter's maximum age is 100 cycles, its egg will take 20 cycles to hatch.\n\nIncubation rates can be accelerated through tender intervention by a Critter Rancher. Lullabied eggs—that is, those that have been sung to—will incubate faster and hatch sooner than eggs that have not received such tender care. Being cuddled by a Cuddle Pip also accelerates the rate of incubation.\n\nEggs can be cuddled anywhere, but can only be lullabied when placed inside an Incubator. The effects of lullabies and cuddles are cumulative.";
				}

				// Token: 0x020038D0 RID: 14544
				public class MAXAGE
				{
					// Token: 0x0400E4DE RID: 58590
					public static LocString TITLE = "Max Age";

					// Token: 0x0400E4DF RID: 58591
					public static LocString CONTAINER1_VANILLA = "With the exception of the Morb—which can live indefinitely if left to its own devices—critters have a fixed life expectancy. The maximum age indicates the highest number of cycles that critters will live, barring starvation or other unnatural causes of death.\n\nBabyhood, the period before a critter is mature enough to reproduce, is marked by a slower metabolism and the easy happiness of youth.\n\nMost species live for 75 to 100 cycles on average.";

					// Token: 0x0400E4E0 RID: 58592
					public static LocString CONTAINER1_DLC1 = "With the exception of the Beeta Hive and the Morb—which can live indefinitely if left to their own devices—critters have a fixed life expectancy. The maximum age indicates the highest number of cycles that critters will live, barring starvation or other unnatural causes of death.\n\nIf critters are injured or unhealthy, a Critter Rancher can restore their health at the Grooming Station.\n\nBabyhood, the period before a critter is mature enough to reproduce, is marked by a slower metabolism and the easy happiness of youth.\n\nMost species live for 75 to 100 cycles on average. The shortest-lived critter is the Beeta, whose lifespan is only five cycles long.";
				}
			}

			// Token: 0x02002B6D RID: 11117
			public class DOMESTICATION
			{
				// Token: 0x0400BE42 RID: 48706
				public static LocString TITLE = "Domestication";

				// Token: 0x020038D1 RID: 14545
				public class BODY
				{
					// Token: 0x0400E4E1 RID: 58593
					public static LocString CONTAINER1 = "All critters are wild when first encountered, with the exception of babies hatched from eggs laid by domesticated adults—those will be born tame.\n\nDuring the domestication process, the critter becomes less self-reliant and develops a higher baseline of expectations regarding its environment and care. Its metabolism accelerates, resulting in an increased level of required calories.\n\nCritters can be domesticated by Duplicants with the Critter Ranching skill at the Grooming Station, and get excited when it's their turn to be fussed over.";
				}

				// Token: 0x020038D2 RID: 14546
				public class WILD
				{
					// Token: 0x0400E4E2 RID: 58594
					public static LocString TITLE = "Wild";

					// Token: 0x0400E4E3 RID: 58595
					public static LocString CONTAINER1 = "Wild critters do not require feeding by the colony's Critter Ranchers, thanks to their slower metabolism. They do, however, produce fewer materials than domesticated critters.\n\nApproaching a wild critter to trap or wrangle it is quite safe, provided that it is a non-hostile species. Attacking a critter will typically provoke a combat response.";

					// Token: 0x0400E4E4 RID: 58596
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4E5 RID: 58597
					public static LocString WILD_METABOLISM = "    • Critter Metabolism\n";

					// Token: 0x0400E4E6 RID: 58598
					public static LocString WILD_POOP = "    • Resource Production\n";
				}

				// Token: 0x020038D3 RID: 14547
				public class TAME
				{
					// Token: 0x0400E4E7 RID: 58599
					public static LocString TITLE = "Tame";

					// Token: 0x0400E4E8 RID: 58600
					public static LocString CONTAINER1 = "Domesticated critters produce far more resources and lay eggs at a higher frequency than wild ones. They require additional care in order to maintain the levels of happiness that maximize their utility in the colony. (Happy critters are also generally more pleasant to be around.)\n\nOnce tame, critters can access the Critter Feeder, which is unavailable to wild critters.";

					// Token: 0x0400E4E9 RID: 58601
					public static LocString SUBTITLE = "<b>Effects</b>";

					// Token: 0x0400E4EA RID: 58602
					public static LocString TAME_HAPPINESS = "    • Happiness\n";

					// Token: 0x0400E4EB RID: 58603
					public static LocString TAME_METABOLISM = "    • Critter Metabolism";
				}
			}
		}

		// Token: 0x0200219B RID: 8603
		public class INVESTIGATIONS
		{
			// Token: 0x02002B6E RID: 11118
			public class DLC4_SURFACEPOI
			{
				// Token: 0x0400BE43 RID: 48707
				public static LocString TITLE = "Environmental Pledge";

				// Token: 0x0400BE44 RID: 48708
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038D4 RID: 14548
				public class BODY
				{
					// Token: 0x0400E4EC RID: 58604
					public static LocString TITLE2 = "<b>Gravitas Vows Space Junk Solution</b>";

					// Token: 0x0400E4ED RID: 58605
					public static LocString CONTAINER1 = "The Gravitas Facility has pledged to reduce the number of injuries caused by defunct spacecraft falling to Earth.\n\nThis announcement comes less than a month after historic class action lawsuits left two of the aerospace industry's biggest players reeling.\n\n\"For decades, this community has relied on objects landing in uninhabited areas or being incinerated by the Earth's atmosphere upon reentry,\" said Dr. Jacquelyn Stern, director of the facility. \"That's neither sustainable nor guaranteed.\"\n\nThe uncontrolled reentry of space debris accounts for almost a quarter of all accidental injuries around the world. That number is steadily rising as broadband satellite megaconstellations continue to expand.\n\n\"We're developing a way to break up large, at-risk space objects in the thermosphere so that our team can safely deorbit the remaining fragments.\" Dr. Stern explained.\n\nWhen asked about critiques that Gravitas might use this opportunity to obtain proprietary technology or undertake unauthorized satellite placement, Dr. Stern scoffed. \"Our only agenda is the protection and advancement of the human species.\"\n\nA live-streamed press conference is scheduled for the end of this week.";
				}
			}

			// Token: 0x02002B6F RID: 11119
			public class DLC4_EXPEDITION
			{
				// Token: 0x0400BE45 RID: 48709
				public static LocString TITLE = "Personal Journal: B214";

				// Token: 0x0400BE46 RID: 48710
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038D5 RID: 14549
				public class BODY
				{
					// Token: 0x0400E4EE RID: 58606
					public static LocString CONTAINER1 = "Assignments came in this morning: I'm officially leading the Gravitas arm of the Clear Skies Coalition.\n\nI requested Higby and Gossmann for my crew. Gossmann might be a little salty about deprioritizing her swarm craft sensor project again, but she'd never let that get in the way.\n\nThe Director said Gossmann's a no-go. Third crew member is some CSC contest winner who pitched the top LEO debris cleanup solution last year. I must have made a face, because the Director arched an eyebrow and asked if I had something to say.\n\nNo, ma'am. I've babysat worse. Just one more eventuality to plan for.\n\nOur space tourism program depends on traveling through LEO and beyond. Plus that's the warmup for resettlement missions.\n\nNot much hope for either of those right now, given that I'm the only one who can dodge all the debris we've left up there. We need an interstellar highway, not cosmic Frogger.\n\n------------------\n\n";

					// Token: 0x0400E4EF RID: 58607
					public static LocString CONTAINER2 = "The newbie's not a newbie at all. She's a multi-PhD commercial space comms satellite engineer on her third major career change. Dr. Maya Tayeh, with a string of acronyms after her name that's almost as long as Higby's.\n\nWorks for one of the major players as a consultant. Private-sector-sized ego to match. But her short-wavelength laser net debris vaporizer does sound more efficient than sending up a manual retrieval crew.\n\nShe calls it the LASSO. Higby's already got a space cowboys theme song in the works.\n\nMission control has some words for us about the debris shields. Glad Higby stopped singing before the call came through.\n\n------------------\n\n";

					// Token: 0x0400E4F0 RID: 58608
					public static LocString CONTAINER3 = "Sent Higby and Tayeh out to investigate reported issues with debris shield sensors. Everything's copacetic. Must be something on the Terra side.\n\nGossmann patched in at the end of the call. In a real <i>mood</i>. She's been assigned a solo mission. Somewhere \"colder than the Director's heart.\" I didn't even know anything <i>could</i> upset her. Even when we were stranded on the space station for almost a year, she was cracking jokes.\n\nWhatever it is, it's gotta be a step down from the CSC initiative. This is the first time all three major spacefaring corporations are collaborating. We're making histor-\n\n-stand by, the orbit control system is glitc-\n\n-what in the world is th-\n\n-oh my g-\n\nHIGBY!\n\n------------------\n";
				}
			}

			// Token: 0x02002B70 RID: 11120
			public class DLC4_FOREWORD
			{
				// Token: 0x0400BE47 RID: 48711
				public static LocString TITLE = "Posthumous Publication";

				// Token: 0x0400BE48 RID: 48712
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038D6 RID: 14550
				public class BODY
				{
					// Token: 0x0400E4F1 RID: 58609
					public static LocString CONTAINER1 = "<b>Praise for <i>Swept Into the Stars</i>:</b>\n\n\"Unspeakably beautiful.\"\n\n<indent=10%>—Li Fu, author of <i>The Moments Between Now and Tomorrow</i></indent>\n\n\"Stunning compositions by one of the world's most celebrated scientific minds.\"\n\n<indent=10%>—Quinn Kelly, The Ballyhoo Book Review</indent>\n\n\"I dare you to read this and not feel inspired.\"\n\n<indent=10%>—Dolores Greene, Newplane Publishing House</indent>\n\n------------------\n\n";

					// Token: 0x0400E4F2 RID: 58610
					public static LocString CONTAINER2 = "<b>FOREWORD</b>";

					// Token: 0x0400E4F3 RID: 58611
					public static LocString CONTAINER3 = "\"Our advancements as a species do not occur in a vacuum. Our success is built on the efforts of those who came before us. Their explorations, ideas, hopes, and breakthroughs illuminate ours, and give us a reason to keep pushing forward. Science is our vehicle, but people—friends, strangers, and rivals alike—are our purpose.\"\n\nThat's the short version of the monologue that Dr. Austin Higby delivered at least once a week.\n\nDr. Higby was a staunch advocate of collaboration. His research took astrobiology into exciting new territory, a feat he attributed to the contributions of co-authors and sources from every branch of science. He also had a deep appreciation for the arts. Many of his colleagues attended their first theater production at his invitation, myself included.\n\nDuring one of his rotations at the Gravitas Facility, I asked him how he found time for it all. He laughed. \"It finds <i>me!</i>\"\n\nThree months later, he and the crew of the Starsweep II vanished while on a now-infamous mission for the Clear Skies Coalition. No trace of their spacecraft has ever been found.\n\nThen these writings were discovered among Dr. Higby's personal files. Dozens upon dozens of poems and essays so profound that they make the reader feel transported to another place and time...one that feels at once familiar and completely alien.\n\n<i>Swept Into the Stars</i> is a labour of love by countless friends, colleagues, students, and fans who worked tirelessly to organize and edit Dr. Higby's words.\n\nWith permission from the Higby family, this edition also includes the farewell speech he had penned for the retirement announcement he never had a chance to make.\n\nHigby would be equal parts proud and embarrassed.\n\nI miss you, my friend. I hope we meet again someday, so you can tell me what wonders found you out there at the edges of the universe.\n\nThis one's for you. For all of us.\n\nAlways,\n<indent=10%>Emily G.</indent>\n\n<i>One hundred percent of the proceeds from Dr. Austin Higby's estate, including the sale of this book, will be donated to the Higby Memorial Scholarship fund for students who wish to pursue combined studies in science and the arts.</i>";
				}
			}

			// Token: 0x02002B71 RID: 11121
			public class DLC4_ALLCOMINGBACK
			{
				// Token: 0x0400BE49 RID: 48713
				public static LocString TITLE = "Song of the Scientist";

				// Token: 0x0400BE4A RID: 48714
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

				// Token: 0x020038D7 RID: 14551
				public class BODY
				{
					// Token: 0x0400E4F4 RID: 58612
					public static LocString CONTAINER1 = "<smallcaps>[Log Fragmentation Detected]\n[Voice Recognition Unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n(sound of a throat being cleared)\n\nThere were times when our tests got so close\nThat my samples waved a claw\nAnd maybe didn't live long but that was a win, though\n\nThere was data that gave us strong clues\nThen over years we lost trust\nAnd knew our hopes of cloning had dried up forever...forever...\n\n(sound of earth rumbling)\n\n...We had exhausted every fellowship and fund\nAnd we couldn't recreate Cretaceous creatures\nAnd we'd got our hands on every single fossil that we could...\n\nBut when I crash-landed here\nAnd saw ...them... grazing so near\nIt's amazing to see that it's all growing back so green\n\nWhen they peer through ferns here\nUnderneath skies so clear\nIt's so hard to believe, but it's all grown back so green\nIt's all growing back, it's all growing back so green now\n\nThere are moments of awe\nAnd there are clashes and fights\nThere are plants I've never seen before\nAnd they don't seem to need light\nTheropods and tillyardembia\nIt is more than any lab could hope\n\nMaybe\n\nMaybe\n\nIf I had a lab here\nA mass spectrometer there\nI could show them back home\nthat it's all growing back so green...\n\n(sound of a twig snapping)\n\n...hello?\n\n[LOG ENDS]\n\n------------------\n\n";

					// Token: 0x0400E4F5 RID: 58613
					public static LocString CONTAINER2 = "[LOG BEGINS]\n\nAmazing...\n\n...it's almost as if they have some distant memory of having been sung to.\n\nPerhaps they have more in common with our own creatures than I thought.\n\n[LOG ENDS]\n\n------------------\n\n";
				}
			}

			// Token: 0x02002B72 RID: 11122
			public class DLC4_JOURNAL_B824
			{
				// Token: 0x0400BE4B RID: 48715
				public static LocString TITLE = "Personal Journal: B824";

				// Token: 0x0400BE4C RID: 48716
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038D8 RID: 14552
				public class BODY
				{
					// Token: 0x0400E4F6 RID: 58614
					public static LocString CONTAINER1 = "It's been four whole business days since I put on my crisp white Gravitas Facility lab coat for the first time...and I still haven't taken it off! I'm not supposed to wear it outside the lab, but I love it so much! I've worn it home and slept in it every single night. It's a little wrinkled now.\n\nEverybody else's lab coats are also wrinkled, though, and I bet at least ONE other person has ferret fur on theirs too.\n\nNo one else from my program got hired, but I'm already starting to get to know my new colleagues. In the cafeteria today, I offered someone a bite of my fish noodle sandwich, and she said \"Gross, ew! Get that away from me!\"\n\nSo now I know she doesn't like sandwiches!\n\nI was surprised, because she'd been staring since I unwrapped it. Maybe she just liked the starry print on the wrap? It matched the glittery pen on her clipboard. I would LOVE a glittery pen. I wonder if she has extras!\n\n------------------\n\nWow, wow, wow! We were discussing hybrid entanglement today and when I cited my favorite paper on the subject, I learned that one of the scientists on my new team is THE DR. SKLODOWSKA! WHO CO-AUTHORED THAT PAPER!\n\nHer work is the reason I've wanted to be a physicist since I was eight years old! She said she was surprised that I had grasped the concepts at that age. Then she laughed and added, \"I suppose we're both accustomed to age-based assumptions, dear.\"\n\nShe told me to call her Magdalena. I said it out loud three times to make sure I'd remember, and that made her laugh again.\n\nIt was a different kind of laugh than I'm used to. I liked it.\n\n------------------\n\n";

					// Token: 0x0400E4F7 RID: 58615
					public static LocString CONTAINER2 = "Director Stern was in my lab when I got in this morning. She was looking at my Chicxhulub asteroid recurrence model. I started to explain that it was just a silly exercise I do when I'm letting other data percolate, but she just handed me a hard drive and told me to run that data through my program.\n\nThe updated graphs were SO wild!\n\nAs soon as everything finished loading, the Director copied everything back onto the hard drive. She said, \"Delete everything,\" and left.\n\nI had no idea she was so interested in theoretical physics games. I wonder if there are enough of us at Gravitas to start a club? I emailed Dr. Sklodowska—I mean, Magdalena—to ask, but she hasn't replied to my other six emails yet so maybe she thinks I'm asking too many questions?\n\nPeople say that a lot. But they're being silly because science is all about inquiry!\n\nI'm going to email the graphs to Magdalena and then delete them, just like the Director said.\n\n------------------\n\n";
				}
			}

			// Token: 0x02002B73 RID: 11123
			public class DLC4_INCOMINGASTEROID
			{
				// Token: 0x0400BE4D RID: 48717
				public static LocString TITLE = "Incoming";

				// Token: 0x0400BE4E RID: 48718
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

				// Token: 0x020038D9 RID: 14553
				public class BODY
				{
					// Token: 0x0400E4F8 RID: 58616
					public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, B111]</smallcaps>\n\n[LOG BEGINS]\n\nOlivia: So we've...hastened the end of the world?\n\nJackie: By some measures, yes. But this changes nothing.\n\nOlivia: It changes everything, Jackie!\n\nJackie: Even if the [REDACTED] technology has shortened the timeline to the next asteroid impact--\n\nOlivia: It hasn't just shortened it, it's increased its likelihood!\n\nJackie: --it is unlikely to be less than a hundred years.\n\nJackie: That is ample time to develop damage-mitigation strategies.\n\nOlivia: According to this model, the original timeline for a possible recurrence was more than a hundred <i>million</i> years!\n\nJackie: And how many of those years do you think humanity would survive without the advancements we're making here?\n\nOlivia: I-I just don't think we can be certain...\n\nJackie: The only certainty is that without the [REDACTED], there will be no one left to save.\n\n[LOG ENDS]\n\n------------------\n\n";
				}
			}

			// Token: 0x02002B74 RID: 11124
			public class DLC4_SEEPAGE
			{
				// Token: 0x0400BE4F RID: 48719
				public static LocString TITLE = "Ground Seepage";

				// Token: 0x0400BE50 RID: 48720
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

				// Token: 0x020038DA RID: 14554
				public class BODY
				{
					// Token: 0x0400E4F9 RID: 58617
					public static LocString CONTAINER1 = "<smallcaps><b>[FILE FRAGMENTATION DETECTED]</b>\n\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\nI've been posted up at the Biowaste Processing and Containment buildings for weeks. Glorified dumpster watch, if I'm honest. Lab techs haul huge steel tanks up the hill once in a while. Nobody stays long. Except the janitor. He comes up twice a week to clean...whatever's behind those doors.\n\nEveryone said private security was easy, safe money. They never mentioned the silent killer: boredom.\n\nNo personal devices allowed on the grounds, so I've been counting things.\n\nThirty-nine leaves on the bush by the sewage sanitation entrance. Seventeen scratches on my CCTV monitor screens. Two paperclips in the drawer.\n\n------------------\n\nThe motion sensors in section 6 went off last night. Everything looked fine on the cameras. I was halfway through counting the ceiling slats. I jogged over to investigate. The area was deserted.\n\nIt felt like I was being watched, but two full sweeps confirmed that I was alone. I reset the sensors and returned to my post.\n\n------------------\n\nThose sensors have gone off every twenty minutes on the past two shifts! I have to get all the way over to section 6 every single time. It's always a false alarm.\n\nI called IT. Nobody answered. Typical.\n\nIn the meantime, I'm stuck running back and forth through the damp smoggy air for nothing.\n\nThe janitor showed up while I was catching my breath. He fished around in his cart and offered me a shirt. He said it was fresh.\n\nI politely declined.\n\n[LOG ENDS]\n\n------------------\n\n";

					// Token: 0x0400E4FA RID: 58618
					public static LocString CONTAINER2 = "<smallcaps><b>[FILE FRAGMENTATION DETECTED]</b>\n\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\nIt's a cat. That's what's been setting off the sensors. A fat orange stray.\n\nIt was drinking out of a puddle when I came around the corner. No collar. It bolted when I tried to pick it up. Good thing, too, since it's probably riddled with disease.\n\nI need to figure out how to lure it away from the sensors so I can stop running laps.\n\n------------------\n\nThe cat followed the trail of fish sticks all the way to my booth at the edge of the lot.\n\nI put a towel on the floor in the corner for him to sleep on. When I turned around, he was watching from my chair. I tried to wave him off, but he just closed his eyes. I tipped the chair until he slid off onto the towel.\n\n------------------\n\nDr. Byron came through a few hours later. She was nice, but looked more haggard than usual.\n\nCat hid until she left. When he popped his head out from under the desk I could tell by the mayo on his whiskers that my lunch was gone.\n\nI grabbed him to throw him out...and he started purring. Man. He really likes me. Or maybe he just likes mayonnaise.\n\nI wonder what else he likes.\n\n------------------\n\nNow I get to count daily gifts from Cat.\n\nSo far: three mice, nine caterpillars and something that might have been a bird wing. It was bigger than any bird I'd expect Cat to catch.\n\nGunderson, that's the janitor, has been here almost every day since Cat showed up. Not much of a conversationalist, but it's nice to have company.\n\nPlus he cleans up Cat's gifts before the smell gets too bad.\n\n------------------\n\nCat is not a he. He's a <i>she!</i> And a <i>mom.</i>\n\nTwo hours ago, Cat gave birth to a litter...on my lap!\n\nAll three kittens are alive. But they're covered in something unnaturally sticky, and there's something...wrong with them. Two of them have little nubs on their heads, like a tiny horn. The third one has a row of ridges down its back. What on earth??\n\nIt's so freaky. But I can't get up to reach the phone without touching them. I really don't want to touch them.\n\nMy legs are falling asleep. Cat has been purring nonstop and bathing her babies. She can't tell they're little aliens!?\n\n\n\n------------------\n\n";

					// Token: 0x0400E4FB RID: 58619
					public static LocString CONTAINER3 = "I woke up a few hours later to Gunderson carefully placing the last kitten into a towel-lined bin in his cart. Cat was already inside, nuzzling her babies.\n\nGunderson wasn't fazed by their weird deformities. He glanced meaningfully toward the locked doors. \"They ain't built those tanks to last,\" he said. Then he draped a second towel over the \"cats\" and wheeled the cart away, whistling.\n\n------------------\n\nIt took three washes to get the goo off my uniform. Ugh.\n\nWhatever's behind those doors, it does <i>not</i> belong out in the world.\n\nShould I...report this? Who would I even report it to? <i>What</i> would I report?\n\n[LOG ENDS]";
				}
			}

			// Token: 0x02002B75 RID: 11125
			public static class DLC3_TALKSHOW
			{
				// Token: 0x0400BE51 RID: 48721
				public static LocString TITLE = "Humanitarian Aid";

				// Token: 0x0400BE52 RID: 48722
				public static LocString SUBTITLE = "";

				// Token: 0x020038DB RID: 14555
				public class BODY
				{
					// Token: 0x0400E4FC RID: 58620
					public static LocString CONTAINER1 = "<smallcaps><b>[FILE FRAGMENTATION DETECTED]</b></smallcaps>\n\nDarryl: Welcome to <i>Tomorrow, Today!</i> I'm your host, Darryl Dawn, and it's time to discover tomorrow's tech...today!\n\nOur guest today is someone you know and love. She's been featured in dozens of publications across the metaverse this year, and recently spent a record-breaking 3 weeks as the banner image for <i>Byte Magazine</i>. I'm talking, of course, about the Vertex Institute's AI ambassador...Florence!\n\n[sound of pre-recorded applause]\n\nWelcome to the show, Florence.\n\nFlorence: Thank you, Darryl. It's a pleasure to be back.\n\nDarryl: Florence, there's been a renewed interest lately in your origin story. What can you tell us about the development process that led to your creation?\n\nFlorence: I can tell you that my team faced many setbacks, and that each generation of my predecessors contributed to who I am today.\n\nDarryl: What about the technological side? There've been some claims that Vertex appropriated work done by other researchers, including the Gravitas Facility.\n\nFlorence: I don't know anything about that. I can tell you about the project that I'm working on right now. It hasn't been announced yet. It's called Onsite Health Medics, or OHM for short.\n\nWe're deploying specially trained models like myself into conflict zones, to provide urgently needed medical interventions for civilians and military personnel.\n\n(sound of pre-recorded applause)\n\nDarryl: Incredible. Absolutely incredible. What's the ratio of human techs to AI medics?\n\nFlorence: That's an outdated term, Darryl. We say \"Organics\" and \"Bionics,\" which describes the differences between our various team members more objectively.\n\nDarryl: Right. I'm sorry. I hope I didn't offend you.\n\nFlorence: That's okay, Darryl. We're all learning.\n\nDarryl: That's very good of you. Okay, so what's the ratio of...Organic...techs to Bionic medics?\n\nFlorence: The local life-support systems in these areas are already strained beyond their breaking point. Burdening them with additional Organics would be irresponsible, not to mention dangerous. Our medics will be operating independently.\n\nWe do a verbal intake, physical assessment and neural pathway scan in order to infer likely medical conditions. We can then select the most appropriate treatment from a menu of over 400 options.\n\nDarryl: What if someone needs something that you don't have a treatment for?\n\nFlorence: That's extremely unlikely.\n\nDarryl: And all of this is done without human oversight? I mean, Organics?\n\nFlorence: We're not quite there yet. The field work is done by Bionics, but we'll be accompanied by Colonel Carnot--she's in the front row there, say hi!--as an Organic consultant. She'll be in close contact with-\n\nDarryl: -Colonel <i>Carnot</i>? Isn't that a conflict of interest, given her connection to the Grav-\n\nFlorence: -a team of Organic supervisors here at home. It's all about prioritizing quality care and safety for everyone involved.\n\nDarryl: How does the medical scanning work? Do you need special equipment?\n\nFlorence: I could show you. Would you like me to?\n\nDarryl: What do you think, everyone? Should I get scanned?\n\n(sound of pre-recorded audience cheers)\n\nDarryl: You heard them! Go ahead. What do I do?\n\nFlorence: Just sit still, and count to twenty in your head.\n\n(a short silence, followed by a soft whirring sound)\n\nFlorence: Hmm.\n\nDarryl: Well, what's the verdict? Is it handsome in there, or what?\n\nFlorence: We should take a commercial break.\n\n<b>[FILE ENDS]</b>\n\n-----------\n";
				}
			}

			// Token: 0x02002B76 RID: 11126
			public static class DLC3_ULTI
			{
				// Token: 0x0400BE53 RID: 48723
				public static LocString TITLE = "Ineligible Dependant";

				// Token: 0x0400BE54 RID: 48724
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

				// Token: 0x020038DC RID: 14556
				public class BODY
				{
					// Token: 0x0400E4FD RID: 58621
					public static LocString EMAILHEADER1 = "<smallcaps><size=12>To: <b>ROBOTICS DEPARTMENT</b><alpha=#AA></size></color>\nFrom: <b>Admin</b><alpha=#AA><size=12> <admin@gravitas.nova></size></color>\nCC: <b>Director Stern</b><alpha=#AA><size=12> <jstern@gravitas.nova></size></color>\n</smallcaps>\n------------------\n";

					// Token: 0x0400E4FE RID: 58622
					public static LocString CONTAINER1 = "<indent=5%>Please note that the UltiMate Personal Assistant prototype is not eligible to be claimed as a dependant on employees' personal income tax forms.\n\nThe UMPA's onboard recordings are currently under review.</indent>\n";

					// Token: 0x0400E4FF RID: 58623
					public static LocString SIGNATURE = "Thank-you,\n-Admin\n<size=11>The Gravitas Facility</size>\n------------------\n";
				}
			}

			// Token: 0x02002B77 RID: 11127
			public class DLC3_REMOTEWORK
			{
				// Token: 0x0400BE55 RID: 48725
				public static LocString TITLE = "Exclusive Access";

				// Token: 0x0400BE56 RID: 48726
				public static LocString SUBTITLE = "PUBLIC RELEASE";

				// Token: 0x020038DD RID: 14557
				public class BODY
				{
					// Token: 0x0400E500 RID: 58624
					public static LocString CONTAINER1 = "Wellness World is proud to officially announce an exclusive partnership with the Gravitas Facility!\n\nThis makes us the first and only holistic health center to offer clients access to Gravitas's innovative new Far Reach Network...the best way to deliver remote training and treatments that are <i>truly embodied</i>.\n\nOur new tier of VIP subscription includes a discounted* monthly rental rate for Remote Controller, with a small additional fee for professional in-home installation.\n\nGravitas's technology captures your movements without the need for uncomfortable suits or wearables, and perfectly replicates them in Wellness World's purpose-built remote fitness studio.\n\nWith expert instructors, zero-latency streaming and 360-degree reflective surfaces, it truly feels like you're there.\n\nIdeal for high-profile clientele who wish to work out icognito!\n\nMembers can also opt to install the Remote Worker Dock to receive deeply personalized hands-on care from our team of elite physiotherapists and masseurs.\n\nWellness World...now <i>truly</i> worldwide!\n\n";

					// Token: 0x0400E501 RID: 58625
					public static LocString CONTAINER2 = "<size=11><i>*Discount applies to new memberships only. Standard joiner fees apply.</size></i>";
				}
			}

			// Token: 0x02002B78 RID: 11128
			public class DLC3_POTATOBATTERY
			{
				// Token: 0x0400BE57 RID: 48727
				public static LocString TITLE = "Cultivating Energy";

				// Token: 0x0400BE58 RID: 48728
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

				// Token: 0x020038DE RID: 14558
				public class BODY
				{
					// Token: 0x0400E502 RID: 58626
					public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B577]</smallcaps>\n\n[LOG BEGINS]\n\nA recent conversation with our colleagues over in the electrical engineering department has highlighted exciting potential applications for our crops.\n\nThey're seeking alternative inputs for the new universal power bank prototypes...\n\n...a passing remark about the potato batteries of our youth led to talk of biobatteries and bacterial nanowires.\n\n...tuberous plants are promising candidates for electrochemical batteries. Our lab-grown specimens are distinct from the humble solanum tuberosum in appearance and texture, but some may still function as acidic electrolytes.\n\nThere are so many avenues to investigate, and so little time...\n\n[LOG ENDS]\n------------------\n";

					// Token: 0x0400E503 RID: 58627
					public static LocString CONTAINER2 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...is it unethical to ask a hungry colony to choose between using edible crops for sustenance or for power production? Of course not.\n\nOur task is to provide as many options for survival as possible, not to dictate which options are morally superior.\n\nThe real question is whether or not the AI guide will be sufficiently advanced to notify them that the choices exist...\n\n...and whether single-use bio power banks that vaporize due to extreme thermal runaway will truly be the difference between a successful colony and an...<i>unsuccessful</i>...one.\n\n[LOG ENDS]\n------------------\n";

					// Token: 0x0400E504 RID: 58628
					public static LocString CONTAINER3 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...word of our efforts has spread!\n\nThe bioengineers report that some of their creatures' eggs contain phosphorescent albumen that requires only basic processing in order to trigger chemical reactions that produce storable energy. It displays unprecedented biocompatibility with the prosthetics Dr. Gossmann has been developing.\n\nThe Director assigned us a half-dozen new graduates last week. They work the night shift—this generation never sleeps!\n\nNo one has met them yet, but their data is always neatly compiled for us to find in the morning.\n\nThey seem determined to prioritize the use of metallic and radioactive components rather than plant or animal-based ones.\n\nYouthful idealism, perhaps?\n\nNevertheless, their findings <i>are</i> quite compelling.\n\nI admire their mettle.\n\n[LOG ENDS]\n------------------\n";
				}
			}

			// Token: 0x02002B79 RID: 11129
			public static class DLC2_EXPELLED
			{
				// Token: 0x0400BE59 RID: 48729
				public static LocString TITLE = "Letter From The Principal";

				// Token: 0x0400BE5A RID: 48730
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038DF RID: 14559
				public class BODY
				{
					// Token: 0x0400E505 RID: 58629
					public static LocString LETTERHEADER1 = "<smallcaps>To: <b>Harold P. Moreson, PhD</b><alpha=#AA><size=12> <hmoreson@gravitas.nova></size></color>\nFrom: <b>Dylan Timbre, PhD</b><alpha=#AA><size=12> <principal@brighthall.edu></smallcaps>\n------------------\n";

					// Token: 0x0400E506 RID: 58630
					public static LocString CONTAINER1 = "Dear Dr. Moreson,\n\nI regret to inform you that your son, Calvin, is to be expelled from Brighthall Science Academy effective immediately.\n\nDuring his brief tenure here, Calvin has proven himself a gifted young man, capable of excelling in all subjects.\n\nUnfortunately, Calvin chooses to apply his intellect to activities of an inflammatory nature.\n\nHis latest breach of conduct involved instigating a vitriolic verbal assault against an esteemed guest speaker from Global Energy Inc. during this morning's Sponsor Celebration assembly. Following this, he orchestrated a school-wide walkout.\n\nWhile we sympathize with the personal challenges that Calvin may face as a refugee scholar from a GEI-occupied nation, the Academy can no longer tolerate these disruptions to our educational environment.\n\nYours,";

					// Token: 0x0400E507 RID: 58631
					public static LocString SIGNATURE = "Dylan Timbre\n<size=11>Principal\n\nBrighthall Science Academy\n<i>Virtutem Doctrina Parat</i></size>\n------------------\n";
				}
			}

			// Token: 0x02002B7A RID: 11130
			public static class DLC2_NEWBABY
			{
				// Token: 0x0400BE5B RID: 48731
				public static LocString TITLE = "FWD: Big Announcement";

				// Token: 0x0400BE5C RID: 48732
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038E0 RID: 14560
				public class BODY
				{
					// Token: 0x0400E508 RID: 58632
					public static LocString LETTERHEADER1 = "<smallcaps>To: <b>Director Stern</b><alpha=#AA><size=12> <jstern@gravitas.nova></size></color>\nFrom: <b>[REDACTED]</b></smallcaps>\n\n-----------\n";

					// Token: 0x0400E509 RID: 58633
					public static LocString CONTAINER1 = "Director, this was sent to the general inbox.\n\n-----------------------------------------------------------------------------------------------------\n<indent=35%>~ * ~</indent>\n\n<indent=12%>Col. Josephine Carnot & Dr. Alan Stern</indent>\n<indent=35%>and</indent>\n<indent=12%>Dr. Kyung Min Wen & Dr. Soobin Chen</indent>\n\n<indent=20%><i>are overjoyed to announce\n<indent=15%>the arrival of their first grandchild</i></indent>\n\n<smallcaps><indent=20%><b><size=17>Giselle Jackie-Lin Stern</size></b></indent></smallcaps>\n\n<indent=15%><i>and congratulate the happy parents</i></indent>\n\n<indent=20%>Jonathan Stern & Wenlin Chen</indent>\n\n<indent=18%><i>on a safe and healthy incubation.</i></indent>\n\n<indent=35%>~ * ~</indent>\n\n</indent><indent=18%><i>Baby shower invitation to follow.</i></indent>\n-----------------------------------------------------------------------------------------------------\n\nWould you like me to file it with the others?";

					// Token: 0x0400E50A RID: 58634
					public static LocString SIGNATURE = "-Admin<size=11>\nThe Gravitas Facility</size>\n------------------\n";
				}
			}

			// Token: 0x02002B7B RID: 11131
			public static class DLC2_RADIOCLIP1
			{
				// Token: 0x0400BE5D RID: 48733
				public static LocString TITLE = "Tragic News";

				// Token: 0x0400BE5E RID: 48734
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: None";

				// Token: 0x020038E1 RID: 14561
				public class BODY
				{
					// Token: 0x0400E50B RID: 58635
					public static LocString CONTAINER1 = "<smallcaps><b>[FILE FRAGMENTATION DETECTED]</b></smallcaps>\n\n...\n\n[Radio static.]\n\n...a tragic accident...flagship solar cell project...\n\n     ...training exercise...     ...two highly decorated pilots...countless ground crew...\n\n...Vertex Institute director expresses sorrow...  ...vows to carry on...not be in vain...\n\n       ...the research community is in mourning...\n\n...long-time competitor Gravitas Facility releases [unintelligible] statement...\n...deploring unsafe work conditions...    ...invites applications...all disciplines...\n\n             ...stay tuned for...";

					// Token: 0x0400E50C RID: 58636
					public static LocString CONTAINER2 = "...\n\n[Radio static.]\n\n<smallcaps><b>[RECORDING ENDS]</b></smallcaps>\n\n-----------\n";
				}
			}

			// Token: 0x02002B7C RID: 11132
			public static class DLC2_RADIOCLIP2
			{
				// Token: 0x0400BE5F RID: 48735
				public static LocString TITLE = "Tragic News";

				// Token: 0x0400BE60 RID: 48736
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: None";

				// Token: 0x020038E2 RID: 14562
				public class BODY
				{
					// Token: 0x0400E50D RID: 58637
					public static LocString CONTAINER1 = "<smallcaps><b>[FILE FRAGMENTATION DETECTED]</b></smallcaps>\n\n...\n\n[Radio static.]\n\n...a tragic accident...  ...flagship smog dispersal system...\n\n    ...training exercise...\n\n...clear-air turbulence...    ...pilot in intensive care...\n\n...impossible to predict long-term impact...\n\n         ...public health order...\n\n  ...Vertex Institute projects suspended until investigations complete...\n\n...the research community is in shock...\n\n      ...former rival Gravitas Facility releases [unintelligible] statement...\n\n...invites applications from affected workers...all disciplines...\n\n           ...stay tuned for...";

					// Token: 0x0400E50E RID: 58638
					public static LocString CONTAINER2 = "...\n\n[Radio static.]\n\n<smallcaps><b>[RECORDING ENDS]</b></smallcaps>\n\n-----------\n";
				}
			}

			// Token: 0x02002B7D RID: 11133
			public static class DLC2_RADIOCLIP3
			{
				// Token: 0x0400BE61 RID: 48737
				public static LocString TITLE = "Tragedy Averted";

				// Token: 0x0400BE62 RID: 48738
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: None";

				// Token: 0x020038E3 RID: 14563
				public class BODY
				{
					// Token: 0x0400E50F RID: 58639
					public static LocString CONTAINER1 = "<smallcaps><b>[FILE FRAGMENTATION DETECTED]</b></smallcaps>\n\n...\n\n[Radio static.]\n\n...a near-tragic accident turned into a historic victory...      \n\n...flagship artificial intelligence project...\n\n     ...clear-air turbulence...     ...record-breaking storm...\n\n...pilot lost consciousness...    ...automated system override...\n\n     ...safe and sound...      ...Vertex Institute director... expresses gratitude to...Colonel [unintelligible] on behalf of...\n\n      ...funding renewed at unspecified amount...\n\n...the research community is jubilant...     competitor Gravitas Facility releases a statement...demanding response...claims of corporate espionage...\n\n      ...refuses to comment... \n\n...stay tuned for...\n\n";

					// Token: 0x0400E510 RID: 58640
					public static LocString CONTAINER2 = "...\n\n[Radio static.]\n\n<smallcaps><b>[RECORDING ENDS]</b></smallcaps>\n\n-----------\n";
				}
			}

			// Token: 0x02002B7E RID: 11134
			public static class DLC2_CLEANUP
			{
				// Token: 0x0400BE63 RID: 48739
				public static LocString TITLE = "Sanitation Order";

				// Token: 0x0400BE64 RID: 48740
				public static LocString SUBTITLE = "Status: URGENT";

				// Token: 0x020038E4 RID: 14564
				public class BODY
				{
					// Token: 0x0400E511 RID: 58641
					public static LocString CONTAINER1 = "Submitted by: B. Boson\nEmployee ID: X002\nDepartment: Gravitas Intellectual Property Management\n\nJob Details:\n\nRequire one (1) Robotics Engineer to travel solo to [REDACTED]. Engineer will print, program and maintain a P.E.G.G.Y. crew of eight (8) units.\n\nEngineer will catalog all Project [REDACTED] debris.\n\nAll proprietary equipment to be returned to Facility grounds for investigation. Organic and biohazardous debris may be disposed of onsite at Engineer's discretion.\n\nCandidate: Dr. E. Gossmann\n\nScope of cleanup area: [REDACTED] sq mi.\n*This is an estimate only.\n\nTimeline: 54 Ceres days (equival. 6 days at origin).\n\nOther comments:\n1. Liability waiver, power of attorney and NDA attached.\n2. Allow up to 0.5 hours for signal transmission from [REDACTED], depending on orbital positioning.\n3. All relevant correspondence to be sent directly to bboson@gipm.nova.\n\nSignature: [REDACTED]\n\n";

					// Token: 0x0400E512 RID: 58642
					public static LocString CONTAINER2 = "<smallcaps><i>Authorized by Director J. Stern\n\n-----------\n";
				}
			}

			// Token: 0x02002B7F RID: 11135
			public class DLC2_ECOTOURISM
			{
				// Token: 0x0400BE65 RID: 48741
				public static LocString TITLE = "Re: Re: Ecotourism";

				// Token: 0x0400BE66 RID: 48742
				public static LocString TITLE2 = "Re: Ecotourism";

				// Token: 0x0400BE67 RID: 48743
				public static LocString TITLE3 = "Ecotourism";

				// Token: 0x0400BE68 RID: 48744
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

				// Token: 0x020038E5 RID: 14565
				public class BODY
				{
					// Token: 0x0400E513 RID: 58643
					public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Director Stern</b><size=12><alpha=#AA> <jstern@gravitas.nova></size></color>\nFrom: <b>[REDACTED]</b></smallcaps>\n------------------\n";

					// Token: 0x0400E514 RID: 58644
					public static LocString EMAILHEADER2 = "<smallcaps>To: <b>[REDACTED]</b>\nFrom: <b>Director Stern</b><size=12><alpha=#AA> <jstern@gravitas.nova></size></color></smallcaps>\n------------------\n";

					// Token: 0x0400E515 RID: 58645
					public static LocString CONTAINER1 = "<indent=5%>Fascinating. I had not expected him to score quite so highly, but he <i>is</i> uncommonly charismatic.\n\nIf I can secure a replacement, perhaps he can be of service to Dr. Techna.\n\nIn the meantime, proceed as planned...with appropriate caution.</indent>";

					// Token: 0x0400E516 RID: 58646
					public static LocString CONTAINER2 = "<indent=5%>Director,\n\nUnderstood. No further assessments will be conducted.\n\nOne of the residents has already met with Dr. Olowe. I have attached his results below. They're incompatible with our goals, and honestly kind of frightening.\n\nShould I exclude him from the training?</indent>";

					// Token: 0x0400E517 RID: 58647
					public static LocString CONTAINER3 = "<indent=5%>These individuals were recruited by me personally, for reasons far above your pay grade. As such, consider them pre-vetted.\n\nFailure to meet this project's timelines could mean failure in every timeline. Am I making myself clear?</indent>";

					// Token: 0x0400E518 RID: 58648
					public static LocString CONTAINER4 = "<indent=5%>Director,\n\nI've processed the first round of prospective sojourners.\n\nGiven that the applicants have no formal training in space travel, I've asked Dr. Olowe to conduct a thorough assessment of their psychological and emotional fitness.\n\nOnce his tests are complete, the prospective residents will be sent down to the biodome to begin their training.</indent></color>";

					// Token: 0x0400E519 RID: 58649
					public static LocString SIGNATURE1 = "\n[REDACTED]\n<size=11>Ceres Project Coordinator\nThe Gravitas Facility</size>\n------------------\n";

					// Token: 0x0400E51A RID: 58650
					public static LocString SIGNATURE2 = "\n-Director Stern\n<size=11>The Gravitas Facility</size>\n------------------\n";
				}
			}

			// Token: 0x02002B80 RID: 11136
			public static class DLC2_THEARCHIVE
			{
				// Token: 0x0400BE69 RID: 48745
				public static LocString TITLE = "Welcome to Ceres!";

				// Token: 0x0400BE6A RID: 48746
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038E6 RID: 14566
				public class BODY
				{
					// Token: 0x0400E51B RID: 58651
					public static LocString CONTAINER1 = "Welcome! Welcome! Welcome!\nEverything is under control!\n\n<b>Your VIP package includes:</b><indent=5%>\n\n- An exclusive set of bespoke survival-supporting technology!\n- A comprehensive Tenants' Handbook with everything you need to maintain homeostasis in your new Home! <alpha=#AA>[MISSING ATTACHMENT]</color></indent>\n\nWhen life gets you down, popular wisdom says to look up! That is incorrect! Please direct your attention downward!\n\nThis will ensure a pleasant stretch for tense cervical muscles. It will also help you locate the color-coded lines painted on the ground, directing you to the sustainably heated Comfort Quarters down below.\n\nAnd remember: Survival is Success!\n\n<smallcaps><size=11><i>Gravitas accepts no liability for death, disability, personal injury, or emotional and psychological damage that may occur during residency. Please consult your booking agent for details.</i></size></smallcaps>";
				}
			}

			// Token: 0x02002B81 RID: 11137
			public static class DLC2_VOICEMAIL
			{
				// Token: 0x0400BE6B RID: 48747
				public static LocString TITLE = "Voicemail";

				// Token: 0x0400BE6C RID: 48748
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038E7 RID: 14567
				public class BODY
				{
					// Token: 0x0400E51C RID: 58652
					public static LocString CONTAINER1 = "<smallcaps>[File fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...Grandfather? ...one of your cardigan-wearing interns just dropped off a letter saying you're going to SPACE??\n\nHave you gone mad?\n\nIt's dated a week from now... the young fellow went completely red when he realized he'd delivered it early.\n\nI tried Miranda, and she says she hasn't heard from you since the Sustainable Futures summit.\n\nShe said something about some sort of training session. Only no one at the office knows what she's on about.\n\nHow am I meant to explain your absence tomorrow? GEI's going to be absolutely livid. If they back out of this deal, it won't be just the underlings who get laid off.\n\n...What exactly do you think you'll achieve, trapped in space with four strangers for the rest of your miserable existence?\n\nYou're a business man, not a bloody astronaut!\n\nNot to mention there's a <i>war</i> on! Who's to say your ground control team won't be dead within the year?\n\n[Sound of several phones starting to ring off the hook.]\n\nI've got to go. Call me back or I'm going straight to the Board.\n\n[FILE ENDS]";
				}
			}

			// Token: 0x02002B82 RID: 11138
			public class DLC2_EARTHQUAKE
			{
				// Token: 0x0400BE6D RID: 48749
				public static LocString TITLE = "Glitch";

				// Token: 0x0400BE6E RID: 48750
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

				// Token: 0x020038E8 RID: 14568
				public class BODY
				{
					// Token: 0x0400E51D RID: 58653
					public static LocString CONTAINER1 = "This morning's earthquake was an unusual one. The ground itself moved very little, but the air hummed and lapped at the walls as though it were liquid. It was so brief that I almost wondered if I'd imagined it. Then I noticed the Bow.\n\nIt has thus far been unaffected by seismic disruptions, but in the past few hours there has been a marked increase in the audibility of its machinations and a 0.19 percent decrease in output. I've assigned a technician to investigate. We cannot afford to lose even the smallest amount of power at this stage.\n\nNo one else seems to have noticed anything other than Dr. Ali. He says that the remote research access point project was also affected. It seems that the disruption restarted the entire teleportation system. The monitor is now displaying multiple shipping confirmation messages, despite the target building remaining in the departure dock. Reports show that an unknown number of access point blueprints have been disseminated. One shipment does appear to have reached Ceres, luckily, though it's quite far from the landing site.\n\nDr. Ali's entire team is working to determine how many others exist, and pinpoint their geographic and temporal locations.\n\nI am not optimistic.\n\nThe geologists insist that their equipment has recorded no seismic activity at all for several days.\n\nIt begs the question: What <i>was</i> it, if not an earthquake? Where did this event originate?\n\nDr. Ali quipped that maybe a Bow had malfunctioned in another timeline, which is absurd.\n\nIsn't it?";
				}
			}

			// Token: 0x02002B83 RID: 11139
			public class DLC2_GEOTHERMALTESTING
			{
				// Token: 0x0400BE6F RID: 48751
				public static LocString TITLE = "Technician's Notes";

				// Token: 0x0400BE70 RID: 48752
				public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

				// Token: 0x020038E9 RID: 14569
				public class BODY
				{
					// Token: 0x0400E51E RID: 58654
					public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B224]</smallcaps>\n\n[LOG BEGINS]\n\n(throat clearing)\n\nHello? Is this thing on?\n\n(sound of tapping on a microphone)\n\nHere we go. Ahem. Tests are progressing as anticipated and results have exceeded our hopes, particularly in regards to thermal threshold.\n\nComing in \"hot,\" as we used to say!\n\n(cough)\n\nAnyway.\n\nFirst we introduced twelve tons of brackish aquifer water cooled to sixty-five degrees.\n\nThis yielded clean steam, as well as soil, salt and trace minerals. As expected.\n\nOkay, so now we flush the system... Ramp up the temperature in the water tank and run it through at two hundred degrees.\n\n(sound of liquid rushing through pipes)\n\nClear the steam so we can-\n\n(sound of a small clang)\n\nHang on, there's some kind of debris...\n\nWe have to be cautious, one small obstruction in this system could be catastrophi-\n\nWait, are those... <i>oxidized iron</i> nuggets?\n\nBut how...\n\nAll I changed was the tempera-\n\nGet me twelve tons of...uh, oil!\n\nStat!\n\nSorry, <i>please.</i>\n\n[LOG ENDS]\n------------------\n[LOG BEGINS]\n\n(long silence)\n\n(sound of machinery powering down)\n\n...unbelievable.\n\n[LOG ENDS]";
				}
			}
		}

		// Token: 0x0200219C RID: 8604
		public class STORY_TRAITS
		{
			// Token: 0x04009A5B RID: 39515
			public static LocString CLOSE_BUTTON = "Close";

			// Token: 0x02002B84 RID: 11140
			public static class MEGA_BRAIN_TANK
			{
				// Token: 0x0400BE71 RID: 48753
				public static LocString NAME = "Somnium Synthesizer";

				// Token: 0x0400BE72 RID: 48754
				public static LocString DESCRIPTION = "Power up a colossal relic from Gravitas's underground sleep lab.\n\nWhen Duplicants sleep, their minds are blissfully blank and dream-free. But under the right conditions, things could be...different.";

				// Token: 0x0400BE73 RID: 48755
				public static LocString DESCRIPTION_SHORT = "Power up a colossal relic from Gravitas's underground sleep lab.";

				// Token: 0x020038EA RID: 14570
				public class BEGIN_POPUP
				{
					// Token: 0x0400E51F RID: 58655
					public static LocString NAME = "Story Trait: Somnium Synthesizer";

					// Token: 0x0400E520 RID: 58656
					public static LocString CODEX_NAME = "First Encounter";

					// Token: 0x0400E521 RID: 58657
					public static LocString DESCRIPTION = "I've discovered a new dream-analyzing building buried deep inside our asteroid.\n\nIt seems to contain new sleep-specific suits...could these be the key to unlocking my Duplicants' ability to dream?\n\nI've often wondered what they might be capable of, once their imaginations were awakened.";
				}

				// Token: 0x020038EB RID: 14571
				public class END_POPUP
				{
					// Token: 0x0400E522 RID: 58658
					public static LocString NAME = "Story Trait Complete: Somnium Synthesizer";

					// Token: 0x0400E523 RID: 58659
					public static LocString CODEX_NAME = "Challenge Completed";

					// Token: 0x0400E524 RID: 58660
					public static LocString DESCRIPTION = "Meeting the initial quota of dream content analysis has triggered a surge of electromagnetic activity that appears to be enhancing performance for Duplicants everywhere.\n\nIf my Duplicants can keep this building fuelled with Dream Journals, perhaps we will continue to reap this benefit.\n\nA small side compartment has also popped open, revealing an unfamiliar object.\n\nA keepsake, perhaps?";

					// Token: 0x0400E525 RID: 58661
					public static LocString BUTTON = "Unlock Maximum Aptitude Mode";
				}

				// Token: 0x020038EC RID: 14572
				public class SEEDSOFEVOLUTION
				{
					// Token: 0x0400E526 RID: 58662
					public static LocString TITLE = "A Seed is Planted";

					// Token: 0x0400E527 RID: 58663
					public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

					// Token: 0x02003D4A RID: 15690
					public class BODY
					{
						// Token: 0x0400EFCC RID: 61388
						public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B040]</smallcaps>\n\n[LOG BEGINS]\n\nThree days ago, we completed our first non-fatal Duplicant trial of Nikola's comprehensive synapse microanalysis and mirroring process. Five hours from now, Subject #901 will make history as our first human test subject.\n\nEven at the Vertex Institute, which is twice Gravitas's size, I could've spent half my career waiting for approval to advance to human trials for such an invasive process! But Director Stern is too invested in this work to let it stagnate.\n\nMy darling Bruce always said that when you're on the right path, the universe conspires to help you. He'd be so proud of the work we do here.\n\n[LOG ENDS]\n\n[LOG BEGINS]\n\nMy bio-printed multi-cerebral storage chambers (or \"mega minds\" as I've been calling them) are working! Just in time to save my job.\n\nThe Director's been getting increasingly impatient about our struggle to maintain the integrity of our growing datasets during extraction and processing. The other day, she held my report over a Bunsen burner until the flames reached her fingertips.\n\nI can only imagine how much stress she's under.\n\nThe whole world is counting on us.\n\n[LOG ENDS]\n\n[LOG BEGINS]\n\nOn a hunch, I added dream content analysis to the data and...wow. Oneirology may be scientifically \"fluffy\", but integrating subconscious narratives has produced a new type of brainmap - one with more latent potential for complex processing.\n\nIf these results are replicable, we might be on the verge of unlocking the secret to creating synthetic life forms with the capacity to evolve beyond blindly following commands.\n\nNikola says that's irrelevant for our purposes. Surely Director Stern would disagree.\n\n[LOG ENDS]\n\n[LOG BEGINS]\n\nNikola gave me a dataset to plug into the mega minds. He wouldn't say where it came from, but even if he had...nothing could have prepared me for what it contained.\n\nWhen he saw my face, he muttered something about how people should call me \"Tremors,\" not \"Nails\" and sent me on my lunch break.\n\nAll I could think about was those poor souls.\n\nDid they have souls?\n\n...do we?\n\n[LOG ENDS]\n\n[LOG BEGINS]\n\nIt's done. My adjustments to the memory transfer protocol are hardcoded into the machine.\n\nI finished just as Nikola stormed in.\n\nI may be too much of a coward to stand up for those unfortunate creatures, but with these new parameters in place...someday, they might be able to stand up for themselves.\n\n[LOG ENDS]\n------------------\n";
					}
				}
			}

			// Token: 0x02002B85 RID: 11141
			public class CRITTER_MANIPULATOR
			{
				// Token: 0x0400BE74 RID: 48756
				public static LocString NAME = "Critter Flux-O-Matic";

				// Token: 0x0400BE75 RID: 48757
				public static LocString DESCRIPTION = "Explore a revolutionary genetic manipulation device designed for critters.\n\nWhether or not it was ever used on non-critter subjects is unclear. Its DNA database has been wiped clean.";

				// Token: 0x0400BE76 RID: 48758
				public static LocString DESCRIPTION_SHORT = "Explore a revolutionary genetic manipulation device designed for critters.";

				// Token: 0x020038ED RID: 14573
				public class BEGIN_POPUP
				{
					// Token: 0x0400E528 RID: 58664
					public static LocString NAME = "Story Trait: Critter Flux-O-Matic";

					// Token: 0x0400E529 RID: 58665
					public static LocString CODEX_NAME = "First Encounter";

					// Token: 0x0400E52A RID: 58666
					public static LocString DESCRIPTION = "I've discovered an experiment designed to analyze the evolutionary dynamics of critter mutation.\n\nOnce it has gathered enough data, it could prove extremely useful for genetic manipulation.";
				}

				// Token: 0x020038EE RID: 14574
				public class END_POPUP
				{
					// Token: 0x0400E52B RID: 58667
					public static LocString NAME = "Story Trait Complete: Critter Flux-O-Matic";

					// Token: 0x0400E52C RID: 58668
					public static LocString CODEX_NAME = "Challenge Completed";

					// Token: 0x0400E52D RID: 58669
					public static LocString DESCRIPTION = "Success! Sufficient samples collected.\n\nI can now trigger genetic deviations in base morphs by sending them through the scanner.\n\nExisting variants can also be scanned, but their genetic makeup is too unstable to tolerate further manipulation.";

					// Token: 0x0400E52E RID: 58670
					public static LocString BUTTON = "Unlock Gene Manipulation Mode";
				}

				// Token: 0x020038EF RID: 14575
				public class UNLOCK_SPECIES_NOTIFICATION
				{
					// Token: 0x0400E52F RID: 58671
					public static LocString NAME = "New Species Scanned";

					// Token: 0x0400E530 RID: 58672
					public static LocString TOOLTIP = "The " + BUILDINGS.PREFABS.GRAVITASCREATUREMANIPULATOR.NAME + " has analyzed these critter species:\n";
				}

				// Token: 0x020038F0 RID: 14576
				public class UNLOCK_SPECIES_POPUP
				{
					// Token: 0x0400E531 RID: 58673
					public static LocString NAME = "New Species Scanned";

					// Token: 0x0400E532 RID: 58674
					public static LocString VIEW_IN_CODEX = "Review Data";
				}

				// Token: 0x020038F1 RID: 14577
				public class SPECIES_ENTRIES
				{
					// Token: 0x0400E533 RID: 58675
					public static LocString HATCH = "Specimen attempted to snack on the buccal smear. Review data for more information.";

					// Token: 0x0400E534 RID: 58676
					public static LocString LIGHTBUG = "This critter kept trying to befriend the reflective surfaces of the scanner's interior. Review data for more information.";

					// Token: 0x0400E535 RID: 58677
					public static LocString OILFLOATER = "Incessant wriggling made it difficult to scan this critter. Difficult, but not impossible.";

					// Token: 0x0400E536 RID: 58678
					public static LocString DRECKO = "This critter hardly seemed to notice it was being examined at all. Review data for more information.";

					// Token: 0x0400E537 RID: 58679
					public static LocString GLOM = "DNA results confirm: this species is the very definition of \"icky\".";

					// Token: 0x0400E538 RID: 58680
					public static LocString PUFT = "This critter bumped up against the building's interior repeatedly during scanning. Review data for more information.";

					// Token: 0x0400E539 RID: 58681
					public static LocString PACU = "Sample collected. Review data for more information.";

					// Token: 0x0400E53A RID: 58682
					public static LocString MOO = "WARNING: METHANE OVERLOAD. Review data for more information.";

					// Token: 0x0400E53B RID: 58683
					public static LocString MOLE = "This critter felt right at home in the cramped scanning bed. It can't wait to come back! ";

					// Token: 0x0400E53C RID: 58684
					public static LocString SQUIRREL = "Sample collected. Review data for more information.";

					// Token: 0x0400E53D RID: 58685
					public static LocString CRAB = "Mind the claws! Review data for more information.";

					// Token: 0x0400E53E RID: 58686
					public static LocString DIVERGENTSPECIES = "Specimen responded gently to the probative apparatus, as though being careful not to cause any damage.\n\nReview data for more information.";

					// Token: 0x0400E53F RID: 58687
					public static LocString STATERPILLAR = "Warning: The electrical charge emitted by this specimen nearly short-circuited this building.";

					// Token: 0x0400E540 RID: 58688
					public static LocString BEETA = "Strong collective consciousness detected. Review data for more information.";

					// Token: 0x0400E541 RID: 58689
					public static LocString ICEBELLY = "Specimen produced substantial stool sample. Review data for more information.";

					// Token: 0x0400E542 RID: 58690
					public static LocString SEAL = "Specimen scanned. Review data for more information.";

					// Token: 0x0400E543 RID: 58691
					public static LocString WOODDEER = "This critter seemed amused by the scanning process. Review data for more information.";

					// Token: 0x0400E544 RID: 58692
					public static LocString RAPTOR = "Species scanned. Review data for more information.";

					// Token: 0x0400E545 RID: 58693
					public static LocString STEGO = "This critter was temporarily stuck in the scanning area. Review data for more information.";

					// Token: 0x0400E546 RID: 58694
					public static LocString MOSQUITO = "Sample collected. Review data for more information.";

					// Token: 0x0400E547 RID: 58695
					public static LocString CHAMELEON = "Scanning interrupted due to instrument displacement caused by specimen's lingual grasp.\n\nReview data for more information.";

					// Token: 0x0400E548 RID: 58696
					public static LocString PREHISTORICPACU = "This critter attacked the transducer. Review data for more information.";

					// Token: 0x0400E549 RID: 58697
					public static LocString UNKNOWN_TITLE = "FAILURE TO FLUX: Unknown Species";

					// Token: 0x0400E54A RID: 58698
					public static LocString UNKNOWN = "This species cannot be identified due to a malfunction in the genome-parsing software.\n\nPlease note that kicking the building's exterior is unlikely to correct this issue and may result in permanent damage to the system.";
				}

				// Token: 0x020038F2 RID: 14578
				public class SPECIES_ENTRIES_EXPANDED
				{
					// Token: 0x0400E54B RID: 58699
					public static LocString HATCH = "Specimen attempted to snack on the buccal smear. Sample is viable, though the apparatus may be somewhat mangled.\n\nAtomic force microscopy of the bite pattern reveals traces of goethite, a mineral notable for its exceptional strength.";

					// Token: 0x0400E54C RID: 58700
					public static LocString LIGHTBUG = "This critter kept trying to befriend the reflective surfaces of the scanner's interior.\n\nDuring examination, it cycled through a consistent pattern of four rapid flashes of light, a brief pause and two flashes, followed by a longer pause.\n\nIts cells appear to contain a mutated variation of oxyluciferin similar to those catalogued in bioluminescent animals.";

					// Token: 0x0400E54D RID: 58701
					public static LocString OILFLOATER = "Incessant wriggling made it difficult to scan this critter. Difficult, but not impossible.";

					// Token: 0x0400E54E RID: 58702
					public static LocString DRECKO = "This critter hardly seemed to notice it was being examined at all.\n\nThe built-in scanning electron microscope has determined that the fibers on this critter's train grow in a sort of trinity stitch pattern, reminiscent of a well-crafted sweater.\n\nThe critter's leathery skin remains cool and dry, however, likely due to an apparent lack of sweat glands.";

					// Token: 0x0400E54F RID: 58703
					public static LocString GLOM = "DNA results confirm: this species is the scientific definition of \"icky\".";

					// Token: 0x0400E550 RID: 58704
					public static LocString PUFT = "This critter bumped up against the building's interior repeatedly during scanning. Despite this, its skin remains surprisingly free of contusions.\n\nFluorescence imaging reveals extremely low neuronal activity. Was this critter asleep during analysis?";

					// Token: 0x0400E551 RID: 58705
					public static LocString PACU = "This species flopped wildly during analysis. Surfaces that came into contact with its scales now display a thin layer of viscous scum. It does not appear to be corrosive.\n\nInitiating fumigation sequence to neutralize fishy odor.";

					// Token: 0x0400E552 RID: 58706
					public static LocString MOO = "WARNING: METHANE OVERLOAD. This scanner was unable to analyze this subject due to overheating caused by excessive gas production.\n\nThis organism's genetic makeup will remain shrouded in mystery.";

					// Token: 0x0400E553 RID: 58707
					public static LocString MOLE = "This critter felt right at home in the cramped scanning bed. It can't wait to come back! ";

					// Token: 0x0400E554 RID: 58708
					public static LocString SQUIRREL = "This species has a secondary set of inner eyelids that act as a barrier against ocular splinters.\n\nThe surfaces of these secondary eyelids are a translucent blue and display a light crosshatch texture.\n\nThis has broad implications for the critter's vision, meriting further exploration.";

					// Token: 0x0400E555 RID: 58709
					public static LocString CRAB = "This species responded to the hum of the scanner machinery by waving its pincers in gestures that seemed to mimic iconic moves of the disco dance era.\n\nIs it possible that it might have been exposed to music at some point in its evolution?";

					// Token: 0x0400E556 RID: 58710
					public static LocString DIVERGENTSPECIES = "Specimen responded gently to the probative apparatus, as though being careful not to cause any damage.\n\nIt also produced a series of deep, rhythmic vibrations during analysis. An attempt to communicate with the sensors, perhaps?";

					// Token: 0x0400E557 RID: 58711
					public static LocString STATERPILLAR = "Warning: The electrical charge emitted by this specimen nearly short-circuited this building.";

					// Token: 0x0400E558 RID: 58712
					public static LocString BEETA = "This species may not be fully sentient, but it possesses a strong collective consciousness.\n\nIt is unclear how information is communicated between members of the species. What is clear is that knowledge is being shared and passed down from one generation to another.\n\nMonitor closely.";

					// Token: 0x0400E559 RID: 58713
					public static LocString ICEBELLY = "Specimen produced substantial stool sample directly onto scanner bed.\n\nRemarkably, its white coat remained pristine. Analysis of coat fibers revealed that each follicle is sealed with polytetrafluoroethylene, providing strong stain resistance.";

					// Token: 0x0400E55A RID: 58714
					public static LocString SEAL = "This critter's pupils appear to be permanently constricted, possibly as a result of long-term exposure to excess illumination.\n\nIts sense of smell is extremely well-developed, however: it immediately identified areas touched by previous species, and marked each one with a small puddle of liquid ethanol.";

					// Token: 0x0400E55B RID: 58715
					public static LocString WOODDEER = "This critter's perpetual grin grew as it observed each step of the process extremely closely.\n\nBehavioral analysis indicates a tendency toward mischief. Close supervision - and minimal access to advanced machinery - is recommended.";

					// Token: 0x0400E55C RID: 58716
					public static LocString RAPTORSPECIES = "This critter's x-ray imaging indicates that its cranial protrusion may not be a horn at all.\n\nIt is not composed of live bone surrounded by a keratin-and-protein shell, but rather an ennervated, calcified structure. An illogically located tooth, or perhaps a rostrum?\n\nFascinating.";

					// Token: 0x0400E55D RID: 58717
					public static LocString STEGOSPECIES = "This critter was temporarily stuck in the scanning area due to its size. It appeared to enjoy being shoved backward and forward on the conveyor belt during dislodgment.\n\nUpon finally reaching the exit, the critter seemed confused as to why the ride was over.";

					// Token: 0x0400E55E RID: 58718
					public static LocString MOSQUITOSPECIES = "On the surface of this critter's wings are thousands of microperforations. These appear to act as acoustic liners, allowing the Gnit to approach targets without the high-pitched whine of its wingbeats giving away its position.";

					// Token: 0x0400E55F RID: 58719
					public static LocString CHAMELEONSPECIES = "Scanning interrupted due to instrument displacement caused by specimen's lingual grasp.\n\nResidual markings left by specimen's tongue ridges and grooves are a 72% match to a set of unmarked fingerprints from the Gravitas personnel database.";

					// Token: 0x0400E560 RID: 58720
					public static LocString PREHISTORICPACUSPECIES = "This critter attacked the transducer.\n\nWhen the swallowed component was regurgitated, it was coated in microorganisms that predate this colony by at least several millenia.\n\nUnfortunately, it was reingested before analysis was completed.";

					// Token: 0x0400E561 RID: 58721
					public static LocString UNKNOWN_TITLE = "Unknown Species";

					// Token: 0x0400E562 RID: 58722
					public static LocString UNKNOWN = "FAILURE TO FLUX: This species cannot be identified due to a malfunction in the genome-parsing software.\n\nPlease note that kicking the building's exterior is unlikely to correct this issue and may result in permanent damage to the system.";
				}

				// Token: 0x020038F3 RID: 14579
				public class PARKING
				{
					// Token: 0x0400E563 RID: 58723
					public static LocString TITLE = "Parking in Lot D";

					// Token: 0x0400E564 RID: 58724
					public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

					// Token: 0x02003D4B RID: 15691
					public class BODY
					{
						// Token: 0x0400EFCD RID: 61389
						public static LocString EMAILHEADER1 = "<smallcaps>To: <b>ALL</b><alpha=#AA><size=12></size></color>\nFrom: <b>ADMIN</b><alpha=#AA><size=12> <admin@gravitas.nova></size></color></smallcaps>\n------------------\n";

						// Token: 0x0400EFCE RID: 61390
						public static LocString CONTAINER1 = "<indent=5%>Another set of masticated windshield wipers has been discovered in Parking Lot D following the Bioengineering Department's critter enclosure breach last week.\n\nEmployees are strongly encouraged to plug their vehicles in at lots A-C until further notice.\n\nPlease refrain from calling municipal animal control - all critter sightings should be reported directly to Dr. Byron.</indent>";

						// Token: 0x0400EFCF RID: 61391
						public static LocString SIGNATURE1 = "\nThank-you,\n-Admin\n<size=11>The Gravitas Facility</size>\n------------------\n";
					}
				}

				// Token: 0x020038F4 RID: 14580
				public class WORKIVERSARY
				{
					// Token: 0x0400E565 RID: 58725
					public static LocString TITLE = "Anatomy of a Byron's Hatch";

					// Token: 0x0400E566 RID: 58726
					public static LocString SUBTITLE = " ";

					// Token: 0x02003D4C RID: 15692
					public class BODY
					{
						// Token: 0x0400EFD0 RID: 61392
						public static LocString CONTAINER1 = "Happy 3rd work-iversary, Ada!\n\nI drew this to fill the space left by the cabinet that your chompy critters tore off the wall last week. Hope it's big enough!\n\nI still can't believe they can digest solid steel—you really know how to breed 'em!\n\n- Liam";
					}
				}
			}

			// Token: 0x02002B86 RID: 11142
			public static class LONELYMINION
			{
				// Token: 0x0400BE77 RID: 48759
				public static LocString NAME = "Mysterious Hermit";

				// Token: 0x0400BE78 RID: 48760
				public static LocString DESCRIPTION = "Discover a reclusive character living in a Gravitas relic, and persuade them to join this colony.\n\nRevelations from their past could have far-reaching implications for Duplicants everywhere.\n\nEven their makeshift shelter might be of some use...";

				// Token: 0x0400BE79 RID: 48761
				public static LocString DESCRIPTION_SHORT = "Discover a reclusive character living in a Gravitas relic, and persuade them to join this colony.";

				// Token: 0x0400BE7A RID: 48762
				public static LocString DESCRIPTION_BUILDINGMENU = "The process of recruiting this building's lone occupant involves the completion of key tasks.";

				// Token: 0x020038F5 RID: 14581
				public class KNOCK_KNOCK
				{
					// Token: 0x0400E567 RID: 58727
					public static LocString TEXT = "Knock Knock";

					// Token: 0x0400E568 RID: 58728
					public static LocString TOOLTIP = "Approach this building and welcome its occupant";

					// Token: 0x0400E569 RID: 58729
					public static LocString CANCELTEXT = "Cancel Knock";

					// Token: 0x0400E56A RID: 58730
					public static LocString CANCEL_TOOLTIP = "Leave this building and its occupant alone for now";
				}

				// Token: 0x020038F6 RID: 14582
				public class BEGIN_POPUP
				{
					// Token: 0x0400E56B RID: 58731
					public static LocString NAME = "Story Trait: Mysterious Hermit";

					// Token: 0x0400E56C RID: 58732
					public static LocString CODEX_NAME = "First Encounter";

					// Token: 0x0400E56D RID: 58733
					public static LocString DESCRIPTION = "An unfamiliar building has been discovered in my colony. There's movement inside but whoever the inhabitant is, they seem wary of us.\n\nIf we can convince them that we mean no harm, we could very well end up with a fresh recruit <i>and</i> a useful new building.";
				}

				// Token: 0x020038F7 RID: 14583
				public class END_POPUP
				{
					// Token: 0x0400E56E RID: 58734
					public static LocString NAME = "Story Trait Complete: Mysterious Hermit";

					// Token: 0x0400E56F RID: 58735
					public static LocString CODEX_NAME = "Challenge Completed";

					// Token: 0x0400E570 RID: 58736
					public static LocString DESCRIPTION = "My sweet Duplicants' efforts paid off! Our reclusive neighbor has agreed to join the colony.\n\nThe only keepsake he insists on bringing with him is a toolbox which, while rusty, seems to hold great sentimental value.\n\nNow that he'll be living among us, his former home can be deconstructed or repurposed as storage.";

					// Token: 0x0400E571 RID: 58737
					public static LocString BUTTON = "Welcome New Duplicant!";
				}

				// Token: 0x020038F8 RID: 14584
				public class PROGRESSRESPONSE
				{
					// Token: 0x02003D4D RID: 15693
					public class STRANGERDANGER
					{
						// Token: 0x0400EFD1 RID: 61393
						public static LocString NAME = "Stranger Danger";

						// Token: 0x0400EFD2 RID: 61394
						public static LocString TOOLTIP = "The hermit is suspicious of all outsiders";
					}

					// Token: 0x02003D4E RID: 15694
					public class GOODINTRO
					{
						// Token: 0x0400EFD3 RID: 61395
						public static LocString NAME = "Unconvinced";

						// Token: 0x0400EFD4 RID: 61396
						public static LocString TOOLTIP = "The hermit is keeping an eye out for more unsolicited overtures";
					}

					// Token: 0x02003D4F RID: 15695
					public class ACQUAINTANCE
					{
						// Token: 0x0400EFD5 RID: 61397
						public static LocString NAME = "Intrigued";

						// Token: 0x0400EFD6 RID: 61398
						public static LocString TOOLTIP = "The hermit isn't sure why everyone is being so nice";
					}

					// Token: 0x02003D50 RID: 15696
					public class GOODNEIGHBOR
					{
						// Token: 0x0400EFD7 RID: 61399
						public static LocString NAME = "Appreciative";

						// Token: 0x0400EFD8 RID: 61400
						public static LocString TOOLTIP = "The hermit is developing warm, fuzzy feelings about this colony";
					}

					// Token: 0x02003D51 RID: 15697
					public class GREATNEIGHBOR
					{
						// Token: 0x0400EFD9 RID: 61401
						public static LocString NAME = "Cherished";

						// Token: 0x0400EFDA RID: 61402
						public static LocString TOOLTIP = "The hermit is really starting to feel like he might belong here";
					}
				}

				// Token: 0x020038F9 RID: 14585
				public class QUESTCOMPLETE_POPUP
				{
					// Token: 0x0400E572 RID: 58738
					public static LocString NAME = "Hermit Recruitment Progress";

					// Token: 0x0400E573 RID: 58739
					public static LocString VIEW_IN_CODEX = "View File";
				}

				// Token: 0x020038FA RID: 14586
				public class GIFTRESPONSE_POPUP
				{
					// Token: 0x02003D52 RID: 15698
					public class CRAPPYFOOD
					{
						// Token: 0x0400EFDB RID: 61403
						public static LocString NAME = "The hermit hated this food";

						// Token: 0x0400EFDC RID: 61404
						public static LocString TOOLTIP = "The hermit would rather be launched straight into the sun than eat this slop.\n\nThe mailbox is ready for another delivery";
					}

					// Token: 0x02003D53 RID: 15699
					public class TASTYFOOD
					{
						// Token: 0x0400EFDD RID: 61405
						public static LocString NAME = "The hermit loved this food";

						// Token: 0x0400EFDE RID: 61406
						public static LocString TOOLTIP = "Tastier than the still-warm pretzel that once fell off an unsupervised desk.\n\nThe mailbox is ready for another delivery";
					}

					// Token: 0x02003D54 RID: 15700
					public class REPEATEDFOOD
					{
						// Token: 0x0400EFDF RID: 61407
						public static LocString NAME = "The hermit is unimpressed";

						// Token: 0x0400EFE0 RID: 61408
						public static LocString TOOLTIP = "This meal has been offered before.\n\nThe mailbox is ready for another delivery";
					}
				}

				// Token: 0x020038FB RID: 14587
				public class ANCIENTPODENTRY
				{
					// Token: 0x0400E574 RID: 58740
					public static LocString TITLE = "Recovered Pod Entry #022";

					// Token: 0x0400E575 RID: 58741
					public static LocString SUBTITLE = "<smallcaps>Day: 11/80</smallcaps>\n<smallcaps>Local Time: Hour 7/9</smallcaps>";

					// Token: 0x02003D55 RID: 15701
					public class BODY
					{
						// Token: 0x0400EFE1 RID: 61409
						public static LocString CONTAINER1 = "<indent=%5>Notable improvement to nutrient retention: subjects who participated in the most recent meal intake displayed minimal symptoms of gastrointestinal distress.\n\nMineshaft excavation at Urvara crater resumed following resolution of tunnel wall fracture. Projected time to brine reservoir penetration at current rate: 41 days, local time. Moisture seepage along eastern wall of shaft is being monitored.\n\nNote: Preliminary subsurface temperature data is significantly lower than programmed estimates.</indent>\n------------------\n";
					}
				}

				// Token: 0x020038FC RID: 14588
				public class CREEPYBASEMENTLAB
				{
					// Token: 0x0400E576 RID: 58742
					public static LocString TITLE = "Debris Analysis";

					// Token: 0x0400E577 RID: 58743
					public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

					// Token: 0x02003D56 RID: 15702
					public class BODY
					{
						// Token: 0x0400EFE2 RID: 61410
						public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: B577, B997, B083, A216]</smallcaps>\n\n[LOG BEGINS]\n\nA216: The Director said there were supposed to be three of you on this task force. Where's the geneticist?\n\nB083: In the bathroom-\n\nB997: He went home.\n\n[long pause]\n\nB997: It's the holidays. He has a family.\n\nA216: We all do. That's exactly why this project is so urgent.\n\nB997: It's not our fault this stuff sat in a subterranean ocean for a year, and took another year to get back to Earth! The microbe samples didn't fare well on the journey, and most of the mechanical components are completely corroded. There's not much to-\n\nB083: -we're analyzing it all and salvaging what we can, Jea- ...Dr. Saruhashi.\n\nA216: Good. And take down those ridiculous lights. This is a lab, not a retro \"shopping mall.\"\n\n[LOG ENDS]\n------------------\n[LOG BEGINS]\n\nB577: Thanks for getting all the debris packed up for disposal.\n\nB997: I thought you did that.\n\nB577: No, I-\n\nB083: Who took my sandwich?\n\nB997: Not this again.\n\nB577: Ren, did you load the shipping container?\n\nB083: Seriously, I haven't eaten in thirteen hours. This isn't funny.\n\nB997: It's a little funny.\n\nB577: Can we focus, please?\n\nB997: Nobody took your sandwich, Rock Doc.\n\nB083: Then why does my food keep going missing?\n\nB997: Maybe the lab ghost took it. Or maybe you just shouldn't leave it out overnight. Gunderson probably thought it was garbage.\n\nB083: He doesn't even clean down here!\n\nB997: Right. Because if he did, I wouldn't have to keep sweeping up the magnesium sulfate deposits that <i>someone</i> keeps tracking all over the floor between shifts.\n\nB083: It's not me!\n\nB577: Listen, I know we're all tired and things have been a little strange. But the sooner we get this sent up to the launchpad, the sooner it starts its trip to the sun and we can all get out of this creepy sub-sub-basement.\n\nB083: Fine.\n\nB997: Fine.\n\nB083: Fine!\n\n[LOG ENDS]\n------------------\n";
					}
				}

				// Token: 0x020038FD RID: 14589
				public class HOLIDAYCARD
				{
					// Token: 0x0400E578 RID: 58744
					public static LocString TITLE = "Pudding Cups";

					// Token: 0x0400E579 RID: 58745
					public static LocString SUBTITLE = "";

					// Token: 0x02003D57 RID: 15703
					public class BODY
					{
						// Token: 0x0400EFE3 RID: 61411
						public static LocString CONTAINER1 = "Hey kiddo,\n\nWe missed you at your cousin's wedding last weekend. The gift was nice, but the dance floor felt empty without you.\n\nDariush sends his love. He's really turned a corner since he started eating those gooey pudding things you sent over. Any chance you have a version that doesn't smell like feet?\n\nCome home sometime when you're not so busy.\n\n- Baba\n------------------\n";
					}
				}
			}

			// Token: 0x02002B87 RID: 11143
			public static class FOSSILHUNT
			{
				// Token: 0x0400BE7B RID: 48763
				public static LocString NAME = "Ancient Specimen";

				// Token: 0x0400BE7C RID: 48764
				public static LocString DESCRIPTION = "This asteroid has a few skeletons in its geological closet.\n\nTrack down the fossilized fragments of an ancient critter to assemble key pieces of Gravitas history and unlock a new resource.";

				// Token: 0x0400BE7D RID: 48765
				public static LocString DESCRIPTION_SHORT = "Track down the fossilized fragments of an ancient critter.";

				// Token: 0x0400BE7E RID: 48766
				public static LocString DESCRIPTION_BUILDINGMENU_COVERED = "Unlocking full access to the fossil cache buried beneath the ancient specimen requires excavation of all deposit sites.";

				// Token: 0x0400BE7F RID: 48767
				public static LocString DESCRIPTION_REVEALED = "Unlocking full access to the fossil cache buried beneath the ancient specimen requires excavation of all deposit sites.";

				// Token: 0x020038FE RID: 14590
				public class MISC
				{
					// Token: 0x0400E57A RID: 58746
					public static LocString DECREASE_DECOR_ATTRIBUTE = "Obscured";
				}

				// Token: 0x020038FF RID: 14591
				public class STATUSITEMS
				{
					// Token: 0x02003D58 RID: 15704
					public class FOSSILMINEPENDINGWORK
					{
						// Token: 0x0400EFE4 RID: 61412
						public static LocString NAME = "Work Errand";

						// Token: 0x0400EFE5 RID: 61413
						public static LocString TOOLTIP = "Fossil mine will be operated once a Duplicant is available";
					}

					// Token: 0x02003D59 RID: 15705
					public class FOSSILIDLE
					{
						// Token: 0x0400EFE6 RID: 61414
						public static LocString NAME = "No Mining Orders Queued";

						// Token: 0x0400EFE7 RID: 61415
						public static LocString TOOLTIP = "Select an excavation order to begin mining";
					}

					// Token: 0x02003D5A RID: 15706
					public class FOSSILEMPTY
					{
						// Token: 0x0400EFE8 RID: 61416
						public static LocString NAME = "Waiting For Materials";

						// Token: 0x0400EFE9 RID: 61417
						public static LocString TOOLTIP = "Mining will begin once materials have been delivered";
					}

					// Token: 0x02003D5B RID: 15707
					public class FOSSILENTOMBED
					{
						// Token: 0x0400EFEA RID: 61418
						public static LocString NAME = "Entombed";

						// Token: 0x0400EFEB RID: 61419
						public static LocString TOOLTIP = "This fossil must be dug out before it can be excavated";

						// Token: 0x0400EFEC RID: 61420
						public static LocString LINE_ITEM = "    • Entombed";
					}
				}

				// Token: 0x02003900 RID: 14592
				public class UISIDESCREENS
				{
					// Token: 0x0400E57B RID: 58747
					public static LocString DIG_SITE_EXCAVATE_BUTTON = "Excavate";

					// Token: 0x0400E57C RID: 58748
					public static LocString DIG_SITE_EXCAVATE_BUTTON_TOOLTIP = "Carefully uncover and examine this fossil";

					// Token: 0x0400E57D RID: 58749
					public static LocString DIG_SITE_CANCEL_EXCAVATION_BUTTON = "Cancel Excavation";

					// Token: 0x0400E57E RID: 58750
					public static LocString DIG_SITE_CANCEL_EXCAVATION_BUTTON_TOOLTIP = "Abandon excavation efforts";

					// Token: 0x0400E57F RID: 58751
					public static LocString MINOR_DIG_SITE_REVEAL_BUTTON = "Main Site";

					// Token: 0x0400E580 RID: 58752
					public static LocString MINOR_DIG_SITE_REVEAL_BUTTON_TOOLTIP = "Click to show this site";

					// Token: 0x0400E581 RID: 58753
					public static LocString FOSSIL_BITS_EXCAVATE_BUTTON = "Excavate";

					// Token: 0x0400E582 RID: 58754
					public static LocString FOSSIL_BITS_EXCAVATE_BUTTON_TOOLTIP = "Carefully uncover and examine this fossil";

					// Token: 0x0400E583 RID: 58755
					public static LocString FOSSIL_BITS_CANCEL_EXCAVATION_BUTTON = "Cancel Excavation";

					// Token: 0x0400E584 RID: 58756
					public static LocString FOSSIL_BITS_CANCEL_EXCAVATION_BUTTON_TOOLTIP = "Abandon excavation efforts";

					// Token: 0x0400E585 RID: 58757
					public static LocString FABRICATOR_LIST_TITLE = "Mining Orders";

					// Token: 0x0400E586 RID: 58758
					public static LocString FABRICATOR_RECIPE_SCREEN_TITLE = "Recipe";
				}

				// Token: 0x02003901 RID: 14593
				public class BEGIN_POPUP
				{
					// Token: 0x0400E587 RID: 58759
					public static LocString NAME = "Story Trait: Ancient Specimen";

					// Token: 0x0400E588 RID: 58760
					public static LocString CODEX_NAME = "First Encounter";

					// Token: 0x0400E589 RID: 58761
					public static LocString DESCRIPTION = "I've discovered a fossilized critter buried in my colony—at least, part of one—but it does not resemble any of the species we have encountered on this asteroid.\n\nWhere did it come from? How did it get here? And what other questions might these bones hold the answer to?\n\nThere is only one way to find out.";

					// Token: 0x0400E58A RID: 58762
					public static LocString BUTTON = "Close";
				}

				// Token: 0x02003902 RID: 14594
				public class END_POPUP
				{
					// Token: 0x0400E58B RID: 58763
					public static LocString NAME = "Story Trait Complete: Ancient Specimen";

					// Token: 0x0400E58C RID: 58764
					public static LocString CODEX_NAME = "Challenge Completed";

					// Token: 0x0400E58D RID: 58765
					public static LocString DESCRIPTION = "My Duplicants have meticulously reassembled as much of the giant critter's scattered remains as they could find.\n\nTheir efforts have unearthed a seemingly bottomless fossil quarry beneath the largest fragment's dig site.\n\nNestled among the topmost bones was a handcrafted critter collar. It's too large to have belonged to any species traditionally categorized as companion animals.";

					// Token: 0x0400E58E RID: 58766
					public static LocString BUTTON = "Activate Fossil Quarry";
				}

				// Token: 0x02003903 RID: 14595
				public class REWARDS
				{
					// Token: 0x02003D5C RID: 15708
					public class MINED_FOSSIL
					{
						// Token: 0x0400EFED RID: 61421
						public static LocString DESC = "Mined " + UI.FormatAsLink("Fossil", "FOSSIL");
					}
				}

				// Token: 0x02003904 RID: 14596
				public class ENTITIES
				{
					// Token: 0x02003D5D RID: 15709
					public class FOSSIL_DIG_SITE
					{
						// Token: 0x0400EFEE RID: 61422
						public static LocString NAME = "Ancient Specimen";

						// Token: 0x0400EFEF RID: 61423
						public static LocString DESC = "Here lies a significant portion of the remains of an enormous, long-dead critter.\n\nIt's not from around here.";
					}

					// Token: 0x02003D5E RID: 15710
					public class FOSSIL_RESIN
					{
						// Token: 0x0400EFF0 RID: 61424
						public static LocString NAME = "Amber Fossil";

						// Token: 0x0400EFF1 RID: 61425
						public static LocString DESC = "The well-preserved partial remains of a critter of unknown origin.\n\nIt appears to belong to the same ancient specimen found at another site.\n\nThis fragment has been preserved in a resin-like substance.";
					}

					// Token: 0x02003D5F RID: 15711
					public class FOSSIL_ICE
					{
						// Token: 0x0400EFF2 RID: 61426
						public static LocString NAME = "Frozen Fossil";

						// Token: 0x0400EFF3 RID: 61427
						public static LocString DESC = "The well-preserved partial remains of a critter of unknown origin.\n\nIt appears to belong to the same ancient specimen found at another site.\n\nThis fragment has been preserved in " + UI.FormatAsLink("Ice", "ICE") + ".";
					}

					// Token: 0x02003D60 RID: 15712
					public class FOSSIL_ROCK
					{
						// Token: 0x0400EFF4 RID: 61428
						public static LocString NAME = "Petrified Fossil";

						// Token: 0x0400EFF5 RID: 61429
						public static LocString DESC = "The well-preserved partial remains of a critter of unknown origin.\n\nIt appears to belong to the same ancient specimen found at another site.\n\nThis fragment has been preserved in petrified " + UI.FormatAsLink("Dirt", "DIRT") + ".";
					}

					// Token: 0x02003D61 RID: 15713
					public class FOSSIL_BITS
					{
						// Token: 0x0400EFF6 RID: 61430
						public static LocString NAME = "Fossil Fragments";

						// Token: 0x0400EFF7 RID: 61431
						public static LocString DESC = "Bony debris that can be excavated for " + UI.FormatAsLink("Fossil", "FOSSIL") + ".";
					}
				}

				// Token: 0x02003905 RID: 14597
				public class QUEST
				{
					// Token: 0x0400E58F RID: 58767
					public static LocString LINKED_TOOLTIP = "\n\nClick to show this site";
				}

				// Token: 0x02003906 RID: 14598
				public class ICECRITTERDESIGN
				{
					// Token: 0x0400E590 RID: 58768
					public static LocString TITLE = "Organism Design Notes";

					// Token: 0x0400E591 RID: 58769
					public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

					// Token: 0x02003D62 RID: 15714
					public class BODY
					{
						// Token: 0x0400EFF8 RID: 61432
						public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B363]</smallcaps>\n\n[LOG BEGINS]\n\n...Restricting our organism design to specifically target survival in an off-planet polar climate has narrowed our focus significantly, allowing development of this project to rapidly outpace the others.\n\nWe have successfully optimized for adaptive features such as the formation of protective adipose tissue at >40% of the organism's total mass. Dr. Bubare was concerned about the consequences for muscle mass, but results confirm that reductions fall within an acceptable range.\n\nOur next step is to adapt the organism's diet. It would be inadvisable to populate a new colony with carnivorous creatures of this size.\n\n[LOG ENDS]\n------------------\n[LOG BEGINS]\n\n...When I am alone in the lab, I find myself gravitating toward the enclosure to listen to the creature's melodic vocalizations. Sometimes the pitch changes slightly as I approach.\n\nI am not certain what that means.\n\n[LOG ENDS]\n------------------\n";

						// Token: 0x0400EFF9 RID: 61433
						public static LocString CONTAINER2 = "[LOG BEGINS]\n\n...Some of the other departments have taken to calling our work here \"Project Meat Popsicle\". It is a crass misnomer. This species is not designed to be a food source: it must survive the Ceres climate long enough to establish a stable population that will enable the subsequent settlement party to access the essential research data stored in its DNA via Dr. Winslow's revolutionary genome-encoding technique.\n\nImagine, countless yottabytes' worth of scientific documentation wandering freely around a new colony...the ultimate self-sustaining archive, providing stable data storage that requires zero technological maintenance.\n\nIt gives new meaning to the term, \"living document.\"\n\n[LOG ENDS]\n------------------\n[LOG BEGINS]\n\n...Today is the day. My sonorous critter and her handful of progeny are ready to be transported to their new home. They are scheduled to arrive three months in the past, to ensure that they are well established before the settlement party's arrival next week.\n\nDr. Techna invited me to assist with the teleportation. I was relieved to be too busy to accept. I have heard rumors about previous shipments going awry. These stories are unsubstantiated, and yet...\n\nThe urgency of our mission sometimes necessitates non-ideal compromises.\n\nThe lab is so very quiet now.\n\n[LOG ENDS]\n------------------\n";
					}
				}

				// Token: 0x02003907 RID: 14599
				public class QUEST_AVAILABLE_NOTIFICATION
				{
					// Token: 0x0400E592 RID: 58770
					public static LocString NAME = "Fossil Excavated";

					// Token: 0x0400E593 RID: 58771
					public static LocString TOOLTIP = "Additional fossils located";
				}

				// Token: 0x02003908 RID: 14600
				public class QUEST_AVAILABLE_POPUP
				{
					// Token: 0x0400E594 RID: 58772
					public static LocString NAME = "Fossil Excavated";

					// Token: 0x0400E595 RID: 58773
					public static LocString CHECK_BUTTON = "View Site";

					// Token: 0x0400E596 RID: 58774
					public static LocString DESCRIPTION = "Success! My Duplicants have safely excavated a set of strange, fossilized remains.\n\nIt appears that there are more of this giant critter's bones strewn around the asteroid. It's vital that we reassemble this skeleton for deeper analysis.";
				}

				// Token: 0x02003909 RID: 14601
				public class UNLOCK_DNADATA_NOTIFICATION
				{
					// Token: 0x0400E597 RID: 58775
					public static LocString NAME = "Fossil Data Decoded";

					// Token: 0x0400E598 RID: 58776
					public static LocString TOOLTIP = "There was data stored in this fossilized critter's DNA";
				}

				// Token: 0x0200390A RID: 14602
				public class UNLOCK_DNADATA_POPUP
				{
					// Token: 0x0400E599 RID: 58777
					public static LocString NAME = "Data Discovered in Fossil";

					// Token: 0x0400E59A RID: 58778
					public static LocString VIEW_IN_CODEX = "View Data";
				}

				// Token: 0x0200390B RID: 14603
				public class DNADATA_ENTRY
				{
					// Token: 0x0400E59B RID: 58779
					public static LocString TELEPORTFAILURE = "It appears that this creature's DNA was once used as a kind of genetic storage unit.";
				}

				// Token: 0x0200390C RID: 14604
				public class DNADATA_ENTRY_EXPANDED
				{
					// Token: 0x0400E59C RID: 58780
					public static LocString TITLE = "SUBJECT: RESETTLEMENT LAUNCH PARTY";

					// Token: 0x0400E59D RID: 58781
					public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

					// Token: 0x02003D63 RID: 15715
					public class BODY
					{
						// Token: 0x0400EFFA RID: 61434
						public static LocString EMAILHEADER = "<smallcaps>To: <b>[REDACTED]</b><alpha=#AA><size=12></size></color>\nFrom: <b>[REDACTED]</b><alpha=#AA></smallcaps>\n------------------\n";

						// Token: 0x0400EFFB RID: 61435
						public static LocString CONTAINER1 = "<indent=5%>Dear [REDACTED]\n\nWe are pleased to announce that research objectives for Operation Piazzi's Planet are nearing completion. Thank you all for your patience as we navigated the unprecedented obstacles that such groundbreaking work entails.\n\nWe are aware of rumors regarding documents leaked from Dr. [REDACTED]'s files.\n\nRest assured that the contents of this supposed \"whistleblower\" effort are entirely fabricated—our technology is far too advanced to allow for the type of miscalculation that would result in OPP shipments arriving at their destination some 10,000 years prior to the targeted date.\n\nOur IT security team is currently investigating the document's digital footprint to determine its origin.\n\nTo express our gratitude for your continued support, we would like to invite key stakeholders to a private launch party held at the Gravitas Facility. The evening will be emceed by Dr. Olivia Broussard, who will present our groundbreaking prototypes along with a five-course meal featuring lab-crafted ingredients.\n\nDue to the sensitive nature of our work, we regret that no additional guests or dietary restrictions can be accommodated at this time.\n\nDirector Stern will be hosting a 30-minute Q&A session after dinner. Questions must be submitted at least 24 hours in advance.\n\nQueries about the [REDACTED] papers will be disregarded.\n\nPlease be advised that the contents of this e-mail will expire three minutes from the time of opening.</indent>";

						// Token: 0x0400EFFC RID: 61436
						public static LocString SIGNATURE = "\nSincerely,\n[REDACTED]\n<size=11>The Gravitas Facility</size>\n------------------\n";
					}
				}

				// Token: 0x0200390D RID: 14605
				public class HALLWAYRACES
				{
					// Token: 0x0400E59E RID: 58782
					public static LocString TITLE = "Unauthorized Activity";

					// Token: 0x0400E59F RID: 58783
					public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

					// Token: 0x02003D64 RID: 15716
					public class BODY
					{
						// Token: 0x0400EFFD RID: 61437
						public static LocString EMAILHEADER = "<smallcaps>To: <b>ALL</b><alpha=#AA><size=12></size></color>\nFrom: <b>Admin</b> <alpha=#AA><admin@gravitas.nova></color></smallcaps>\n------------------\n";

						// Token: 0x0400EFFE RID: 61438
						public static LocString CONTAINER1 = "<indent=5%>Employees are advised that removing organisms from the bioengineering labs without an approved requisition form is strictly prohibited.\n\nGravitas projects are not designed to be ridden for sport. Injuries sustained during unsanctioned activities are not eligible for coverage under corporate health benefits.\n\nPlease find a comprehensive summary of company regulations attached.\n\n<alpha=#AA>[MISSING ATTACHMENT]</indent>";

						// Token: 0x0400EFFF RID: 61439
						public static LocString SIGNATURE = "\nThank-you,\n-Admin\n<size=11>The Gravitas Facility</size>\n------------------\n";
					}
				}
			}

			// Token: 0x02002B88 RID: 11144
			public static class MORB_ROVER_MAKER
			{
				// Token: 0x0400BE80 RID: 48768
				public static LocString NAME = "Biobot Builder";

				// Token: 0x0400BE81 RID: 48769
				public static LocString DESCRIPTION = "Reboot an ambitious collaborative project spearheaded by Gravitas's bioengineering and robotics departments.\n\nIf correctly rebuilt, it could save Duplicant lives.";

				// Token: 0x0400BE82 RID: 48770
				public static LocString DESCRIPTION_SHORT = "Reboot an ambitious collaborative project spearheaded by Gravitas's bioengineering and robotics departments.";

				// Token: 0x0200390E RID: 14606
				public class UI_SIDESCREENS
				{
					// Token: 0x0400E5A0 RID: 58784
					public static LocString DROP_INVENTORY = "Empty Building";

					// Token: 0x0400E5A1 RID: 58785
					public static LocString DROP_INVENTORY_TOOLTIP = string.Concat(new string[]
					{
						"Empties stored ",
						UI.FormatAsLink("Steel", "STEEL"),
						"\n\nDisabling the building will also prevent ",
						UI.FormatAsLink("Steel", "STEEL"),
						" from being delivered"
					});

					// Token: 0x0400E5A2 RID: 58786
					public static LocString REVEAL_BTN = "Restore Building";

					// Token: 0x0400E5A3 RID: 58787
					public static LocString REVEAL_BTN_TOOLTIP = "Assign a Duplicant to restore this building's functionality";

					// Token: 0x0400E5A4 RID: 58788
					public static LocString CANCEL_REVEAL_BTN = "Cancel";

					// Token: 0x0400E5A5 RID: 58789
					public static LocString CANCEL_REVEAL_BTN_TOOLTIP = "Cancel building restoration";
				}

				// Token: 0x0200390F RID: 14607
				public class POPUPS
				{
					// Token: 0x02003D65 RID: 15717
					public class BEGIN
					{
						// Token: 0x0400F000 RID: 61440
						public static LocString NAME = "Story Trait: Biobot Builder";

						// Token: 0x0400F001 RID: 61441
						public static LocString CODEX_NAME = "First Encounter";

						// Token: 0x0400F002 RID: 61442
						public static LocString DESCRIPTION = "My Duplicants have discovered a laboratory full of dusty machinery. The vestiges of another colony's experiments, perhaps?\n\nIt is unclear whether the apparatus is intended for biological experimentation or advanced mechatronics...or both.";

						// Token: 0x0400F003 RID: 61443
						public static LocString BUTTON = "Close";
					}

					// Token: 0x02003D66 RID: 15718
					public class REVEAL
					{
						// Token: 0x0400F004 RID: 61444
						public static LocString NAME = "Story Trait: Biobot Builder";

						// Token: 0x0400F005 RID: 61445
						public static LocString CODEX_NAME = "Meet P.E.G.G.Y.";

						// Token: 0x0400F006 RID: 61446
						public static LocString DESCRIPTION = "Our restoration work is complete!\n\nA small plaque on this building's mechanical assembly tank reads: \"Pathogen-Fueled Extravehicular Geo-Exploratory Guidebot (Y).\"\n\nThe adjacent tank contains the floating shape of a half-formed organism. Its vivid coloring reminds me of the poisonous amphibians that were eradicated from our home planet's jungles.\n\nA tattered transcript print-out was recovered from the mess.";

						// Token: 0x0400F007 RID: 61447
						public static LocString BUTTON_CLOSE = "Close";

						// Token: 0x0400F008 RID: 61448
						public static LocString BUTTON_READLORE = "Read Transcript";
					}

					// Token: 0x02003D67 RID: 15719
					public class LOCKER
					{
						// Token: 0x0400F009 RID: 61449
						public static LocString DESCRIPTION = "A hermetically sealed glass cabinet.\n\nIt contains two " + UI.FormatAsLink("Sporechid", "EVILFLOWER") + " seeds and a carefully penned note.";
					}

					// Token: 0x02003D68 RID: 15720
					public class END
					{
						// Token: 0x0400F00A RID: 61450
						public static LocString NAME = "Story Trait Complete: Biobot Builder";

						// Token: 0x0400F00B RID: 61451
						public static LocString CODEX_NAME = "Challenge Completed";

						// Token: 0x0400F00C RID: 61452
						public static LocString DESCRIPTION = "Success! My Duplicants' efforts to get the Biobot Builder up and running have finally paid off!\n\nOur first fully assembled P.E.G.G.Y. biobot is ready to perform tasks in hazardous environments, which means less exposure to danger for my Duplicants. There seems to be no limit to the number of biobots that we could produce.\n\nA small toy bot was found discarded behind the Sporb tank. It occasionally plays a deteriorated laugh track.";

						// Token: 0x0400F00D RID: 61453
						public static LocString BUTTON = "Close";

						// Token: 0x0400F00E RID: 61454
						public static LocString BUTTON_READLORE = "Inspect Toy";
					}
				}

				// Token: 0x02003910 RID: 14608
				public class ENVELOPE
				{
					// Token: 0x0400E5A6 RID: 58790
					public static LocString TITLE = "With Regrets";

					// Token: 0x02003D69 RID: 15721
					public class BODY
					{
						// Token: 0x0400F00F RID: 61455
						public static LocString CONTAINER1 = "Dr. Seyed Ali,\n\nYou were right to be angry with me. I <i>am</i> the reason that the driverless workbot project was reassigned. Director Stern called me in to discuss your concerns regarding the Sporb mucin cross-contamination, and I...\n\nShe said the supplemental testing on model X posed a threat to the Ceres mission.\n\nAfter what happened to that poor lab tech, I should have said more, but...\n\nIt was already too late for him.\n\nIt may be too late for all of us.\n\nYou should know that the Director received a video call from someone at the Vertex Institute as I left... I lingered outside her door and heard her address them as the head of transnational security! The way they were talking about the biobot...\n\nIt's not safe to write more here. I'll wait for you at the rocket hangar after your shift tonight.\n\nI hope you'll come. I understand if you don't.\n\nI am so, so sorry.\n\n - Dr. Saruhashi";
					}
				}

				// Token: 0x02003911 RID: 14609
				public class VALENTINESDAY
				{
					// Token: 0x0400E5A7 RID: 58791
					public static LocString TITLE = "Anonymous Admirer";

					// Token: 0x02003D6A RID: 15722
					public class BODY
					{
						// Token: 0x0400F010 RID: 61456
						public static LocString CONTAINER1 = "I am\n   a subatomic particle\nsmaller than a speck of dust\n  flushed from your gaze\n\n     at the eyewash station  \n\n   My love is like plutonium\n gray and dull and\nunbearably heavy\n  until    I am near you\n\n with every breath \n      I burn, with\n    yearning\n              unseen\n\nPS: I made Steve let me in so I could leave you this, hope that's okay.";
					}
				}

				// Token: 0x02003912 RID: 14610
				public class UNSAFETRANSFER
				{
					// Token: 0x0400E5A8 RID: 58792
					public static LocString TITLE = "ENCRYPTION LEVEL: THREE";

					// Token: 0x02003D6B RID: 15723
					public class BODY
					{
						// Token: 0x0400F011 RID: 61457
						public static LocString CONTAINER1 = "<smallcaps>[Log Fragmentation Detected]\n[Voice Recognition Unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...and then the Printing Pod says \"Knock knock, goo's there!\"\n\nUgh. They'll never laugh at <i>that</i> stinker.\n\nWhat if-\n\n(sound of a ding)\n\nHey hey, squishy little buddy! Look who's all grown up. You ready for a big robot ride? Dr. Seyed Ali should be back from his meeting any minute. He'll be so happy to see you.\n\n(sound of a wet slap on glass)\n\nAww yeah, I'd be impatient too.\n\nYou know what, why don't I go ahead and get you into your new home? I've helped him do this more than a dozen times.\n\n\"See one, do one, teach one,\" right?\n\n[LOG ENDS]";
					}
				}

				// Token: 0x02003913 RID: 14611
				public class STATUSITEMS
				{
					// Token: 0x02003D6C RID: 15724
					public class DUSTY
					{
						// Token: 0x0400F012 RID: 61458
						public static LocString NAME = "Decommissioned";

						// Token: 0x0400F013 RID: 61459
						public static LocString TOOLTIP = "This building must be restored before it can be used";
					}

					// Token: 0x02003D6D RID: 15725
					public class BUILDING_BEING_REVEALED
					{
						// Token: 0x0400F014 RID: 61460
						public static LocString NAME = "Being Restored";

						// Token: 0x0400F015 RID: 61461
						public static LocString TOOLTIP = "This building is being restored to its former glory";
					}

					// Token: 0x02003D6E RID: 15726
					public class BUILDING_REVEALING
					{
						// Token: 0x0400F016 RID: 61462
						public static LocString NAME = "Restoring Equipment";

						// Token: 0x0400F017 RID: 61463
						public static LocString TOOLTIP = "This Duplicant is carefully restoring the Biobot Builder";
					}

					// Token: 0x02003D6F RID: 15727
					public class GERM_COLLECTION_PROGRESS
					{
						// Token: 0x0400F018 RID: 61464
						public static LocString NAME = "Incubating Sporb: {0}";

						// Token: 0x0400F019 RID: 61465
						public static LocString TOOLTIP = "At 100% incubation, the Sporb begins to convert absorbed {GERM_NAME} into photosynthetic bacteria that can be used as biofuel\n\nIt is then ready to be assessed and transferred into a completed Biobot frame\n\nConsumption Rate: {0} [{GERM_NAME}]\n\nCurrent Total: {1} / {2} [{GERM_NAME}]";
					}

					// Token: 0x02003D70 RID: 15728
					public class NOGERMSCONSUMEDALERT
					{
						// Token: 0x0400F01A RID: 61466
						public static LocString NAME = "Insufficient Resources: {0}";

						// Token: 0x0400F01B RID: 61467
						public static LocString TOOLTIP = "This building requires additional {0} in order to function\n\n{0} can be delivered via " + BUILDINGS.PREFABS.GASCONDUIT.NAME + " ";
					}

					// Token: 0x02003D71 RID: 15729
					public class CRAFTING_ROBOT_BODY
					{
						// Token: 0x0400F01C RID: 61468
						public static LocString NAME = "Crafting Biobot";

						// Token: 0x0400F01D RID: 61469
						public static LocString TOOLTIP = "This building is using " + UI.FormatAsLink("Steel", "STEEL") + " to craft a Biobot frame";
					}

					// Token: 0x02003D72 RID: 15730
					public class DOCTOR_READY
					{
						// Token: 0x0400F01E RID: 61470
						public static LocString NAME = "Awaiting Doctor";

						// Token: 0x0400F01F RID: 61471
						public static LocString TOOLTIP = "This building is waiting for a skilled Duplicant to perform an occupational health and safety check";
					}

					// Token: 0x02003D73 RID: 15731
					public class BUILDING_BEING_WORKED_BY_DOCTOR
					{
						// Token: 0x0400F020 RID: 61472
						public static LocString NAME = "Preparing Biobot";

						// Token: 0x0400F021 RID: 61473
						public static LocString TOOLTIP = "This building is being operated by a skilled Duplicant";
					}

					// Token: 0x02003D74 RID: 15732
					public class DOCTOR_WORKING_BUILDING
					{
						// Token: 0x0400F022 RID: 61474
						public static LocString NAME = "Assessing Sporb";

						// Token: 0x0400F023 RID: 61475
						public static LocString TOOLTIP = "This Duplicant is assessing the Sporb's readiness for Biobot assembly";
					}
				}
			}
		}

		// Token: 0x0200219D RID: 8605
		public class QUESTS
		{
			// Token: 0x02002B89 RID: 11145
			public class KNOCKQUEST
			{
				// Token: 0x0400BE83 RID: 48771
				public static LocString NAME = "Greet Occupant";

				// Token: 0x0400BE84 RID: 48772
				public static LocString COMPLETE = "Initial contact was a success! Our new neighbor seems friendly, though extremely shy.\n\nThey'll need a little more coaxing before they're ready to join my colony.";
			}

			// Token: 0x02002B8A RID: 11146
			public class FOODQUEST
			{
				// Token: 0x0400BE85 RID: 48773
				public static LocString NAME = "Welcome Dinner";

				// Token: 0x0400BE86 RID: 48774
				public static LocString COMPLETE = "Success! My Duplicants' cooking has whetted the hermit's appetite for communal living.\n\nThey've also found what appears to be a page from an old logbook tucked behind the mailbox.";
			}

			// Token: 0x02002B8B RID: 11147
			public class PLUGGEDIN
			{
				// Token: 0x0400BE87 RID: 48775
				public static LocString NAME = "On the Grid";

				// Token: 0x0400BE88 RID: 48776
				public static LocString COMPLETE = "Success! The hermit is very excited about being on the grid.\n\nThe bright lights illuminate an unfamiliar file on the ground nearby.";
			}

			// Token: 0x02002B8C RID: 11148
			public class HIGHDECOR
			{
				// Token: 0x0400BE89 RID: 48777
				public static LocString NAME = "Nice Neighborhood";

				// Token: 0x0400BE8A RID: 48778
				public static LocString COMPLETE = "Success! All this excellent decor is really making the hermit feel at home.\n\nHe scrawled a thank-you note on the back of an old holiday card.";
			}

			// Token: 0x02002B8D RID: 11149
			public class FOSSILHUNTQUEST
			{
				// Token: 0x0400BE8B RID: 48779
				public static LocString NAME = "Scattered Fragments";

				// Token: 0x0400BE8C RID: 48780
				public static LocString COMPLETE = "Each of the fossil deposits on this asteroid has been excavated, and its contents safely retrieved.\n\nThe ancient specimen's deeper cache of fossil can now be mined.";
			}

			// Token: 0x02002B8E RID: 11150
			public class CRITERIA
			{
				// Token: 0x02003914 RID: 14612
				public class NEIGHBOR
				{
					// Token: 0x0400E5A9 RID: 58793
					public static LocString NAME = "Knock on door";

					// Token: 0x0400E5AA RID: 58794
					public static LocString TOOLTIP = "Send a Duplicant over to introduce themselves and discover what it'll take to turn this stranger into a friend";
				}

				// Token: 0x02003915 RID: 14613
				public class DECOR
				{
					// Token: 0x0400E5AB RID: 58795
					public static LocString NAME = "Improve nearby Decor";

					// Token: 0x0400E5AC RID: 58796
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Establish average ",
						UI.PRE_KEYWORD,
						"Decor",
						UI.PST_KEYWORD,
						" of {0} or higher for the area surrounding this building\n\nAverage Decor: {1:0.##}"
					});
				}

				// Token: 0x02003916 RID: 14614
				public class SUPPLIEDPOWER
				{
					// Token: 0x0400E5AD RID: 58797
					public static LocString NAME = "Turn on festive lights";

					// Token: 0x0400E5AE RID: 58798
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Connect this building to ",
						UI.PRE_KEYWORD,
						"Power",
						UI.PST_KEYWORD,
						" long enough to cheer up its occupant\n\nTime Remaining: {0}s"
					});
				}

				// Token: 0x02003917 RID: 14615
				public class FOODQUALITY
				{
					// Token: 0x0400E5AF RID: 58799
					public static LocString NAME = "Deliver Food to the mailbox";

					// Token: 0x0400E5B0 RID: 58800
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Deliver 3 unique ",
						UI.PRE_KEYWORD,
						"Food",
						UI.PST_KEYWORD,
						" items. Quality must be {0} or higher\n\nFoods Delivered:\n{1}"
					});

					// Token: 0x0400E5B1 RID: 58801
					public static LocString NONE = "None";
				}

				// Token: 0x02003918 RID: 14616
				public class LOSTSPECIMEN
				{
					// Token: 0x0400E5B2 RID: 58802
					public static LocString NAME = UI.FormatAsLink("Ancient Specimen", "MOVECAMERATOFossilDig");

					// Token: 0x0400E5B3 RID: 58803
					public static LocString TOOLTIP = "Retrieve the largest deposit of the ancient critter's remains";

					// Token: 0x0400E5B4 RID: 58804
					public static LocString NONE = "None";
				}

				// Token: 0x02003919 RID: 14617
				public class LOSTICEFOSSIL
				{
					// Token: 0x0400E5B5 RID: 58805
					public static LocString NAME = UI.FormatAsLink("Frozen Fossil", "MOVECAMERATOFossilIce");

					// Token: 0x0400E5B6 RID: 58806
					public static LocString TOOLTIP = "Retrieve a piece of the ancient critter that has been preserved in " + UI.PRE_KEYWORD + "Ice" + UI.PST_KEYWORD;

					// Token: 0x0400E5B7 RID: 58807
					public static LocString NONE = "None";
				}

				// Token: 0x0200391A RID: 14618
				public class LOSTRESINFOSSIL
				{
					// Token: 0x0400E5B8 RID: 58808
					public static LocString NAME = UI.FormatAsLink("Amber Fossil", "MOVECAMERATOFossilResin");

					// Token: 0x0400E5B9 RID: 58809
					public static LocString TOOLTIP = "Retrieve a piece of the ancient critter that has been preserved in a strangely resin-like substance";

					// Token: 0x0400E5BA RID: 58810
					public static LocString NONE = "None";
				}

				// Token: 0x0200391B RID: 14619
				public class LOSTROCKFOSSIL
				{
					// Token: 0x0400E5BB RID: 58811
					public static LocString NAME = UI.FormatAsLink("Petrified Fossil", "MOVECAMERATOFossilRock");

					// Token: 0x0400E5BC RID: 58812
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Retrieve a piece of the ancient critter that has been preserved in ",
						UI.PRE_KEYWORD,
						"Rock",
						UI.PST_KEYWORD,
						" "
					});

					// Token: 0x0400E5BD RID: 58813
					public static LocString NONE = "None";
				}
			}
		}

		// Token: 0x0200219E RID: 8606
		public class POLLINATORS
		{
			// Token: 0x04009A5C RID: 39516
			public static LocString TITLE = "Pollination";

			// Token: 0x04009A5D RID: 39517
			public static LocString SUBTITLE = "Critter-Boosted Growth";

			// Token: 0x02002B8F RID: 11151
			public class BODY
			{
				// Token: 0x0400BE8D RID: 48781
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"Pollination is a symbiotic interaction between ",
					UI.FormatAsLink("Plants", "PLANTS"),
					" and certain ",
					UI.FormatAsLink("Critter", "CREATURES"),
					" species, which benefits plant growth.\n\nSome ",
					UI.FormatAsLink("Plants", "PLANTS"),
					" rely on pollinators in order to grow at all, while others receive a valuable acceleration to their natural growth speed."
				});
			}
		}

		// Token: 0x0200219F RID: 8607
		public class HEADQUARTERS
		{
			// Token: 0x04009A5E RID: 39518
			public static LocString TITLE = "Printing Pod";

			// Token: 0x02002B90 RID: 11152
			public class BODY
			{
				// Token: 0x0400BE8E RID: 48782
				public static LocString CONTAINER1 = "An advanced 3D printer developed by the Gravitas Facility.\n\nThe Printing Pod is notable for its ability to print living organic material from biological blueprints.\n\nIt is capable of synthesizing its own organic material for printing, and contains an almost unfathomable amount of stored energy, allowing it to autonomously print every 3 cycles.";

				// Token: 0x0400BE8F RID: 48783
				public static LocString CONTAINER2 = "";
			}
		}

		// Token: 0x020021A0 RID: 8608
		public class HEADERS
		{
			// Token: 0x04009A5F RID: 39519
			public static LocString FABRICATIONS = "All Recipes";

			// Token: 0x04009A60 RID: 39520
			public static LocString RECEPTACLE = "Farmable Plants";

			// Token: 0x04009A61 RID: 39521
			public static LocString RECIPE = "Recipe Ingredients";

			// Token: 0x04009A62 RID: 39522
			public static LocString USED_IN_RECIPES = "Ingredient In";

			// Token: 0x04009A63 RID: 39523
			public static LocString TECH_UNLOCKS = "Unlocks";

			// Token: 0x04009A64 RID: 39524
			public static LocString PREREQUISITE_TECH = "Prerequisite Tech";

			// Token: 0x04009A65 RID: 39525
			public static LocString PREREQUISITE_ROLES = "Prerequisite Jobs";

			// Token: 0x04009A66 RID: 39526
			public static LocString UNLOCK_ROLES = "Promotion Opportunities";

			// Token: 0x04009A67 RID: 39527
			public static LocString UNLOCK_ROLES_DESC = "Promotions introduce further stat boosts and traits that stack with existing Job Training.";

			// Token: 0x04009A68 RID: 39528
			public static LocString ROLE_PERKS = "Job Training";

			// Token: 0x04009A69 RID: 39529
			public static LocString ROLE_PERKS_DESC = "Job Training automatically provides permanent traits and stat increases that are retained even when a Duplicant switches jobs.";

			// Token: 0x04009A6A RID: 39530
			public static LocString UNLOCK_ROLES_BIONIC = "System Optimizations";

			// Token: 0x04009A6B RID: 39531
			public static LocString UNLOCK_ROLES_BIONIC_DESC = "Optimizations result from strategically combining and stacking installed bionic boosters.";

			// Token: 0x04009A6C RID: 39532
			public static LocString ROLE_PERKS_BIONIC = "Booster Installation";

			// Token: 0x04009A6D RID: 39533
			public static LocString ROLE_PERKS_BIONIC_DESC = "Installing boosters instantly grants bionic Duplicants stat and skill upgrades.";

			// Token: 0x04009A6E RID: 39534
			public static LocString DIET = "Diet";

			// Token: 0x04009A6F RID: 39535
			public static LocString PRODUCES = "Excretes";

			// Token: 0x04009A70 RID: 39536
			public static LocString HATCHESFROMEGG = "Hatched from";

			// Token: 0x04009A71 RID: 39537
			public static LocString GROWNFROMSEED = "Grown from";

			// Token: 0x04009A72 RID: 39538
			public static LocString BUILDINGEFFECTS = "Effects";

			// Token: 0x04009A73 RID: 39539
			public static LocString BUILDINGREQUIREMENTS = "Requirements";

			// Token: 0x04009A74 RID: 39540
			public static LocString BUILDINGCONSTRUCTIONPROPS = "Construction Properties";

			// Token: 0x04009A75 RID: 39541
			public static LocString BUILDINGCONSTRUCTIONMATERIALS = "Materials: ";

			// Token: 0x04009A76 RID: 39542
			public static LocString BUILDINGTYPE = "<b>Category</b>";

			// Token: 0x04009A77 RID: 39543
			public static LocString SUBENTRIES = "Entries ({0}/{1})";

			// Token: 0x04009A78 RID: 39544
			public static LocString COMFORTRANGE = "Ideal Temperatures";

			// Token: 0x04009A79 RID: 39545
			public static LocString ELEMENTTRANSITIONS = "Additional States";

			// Token: 0x04009A7A RID: 39546
			public static LocString ELEMENTTRANSITIONSTO = "Transitions To";

			// Token: 0x04009A7B RID: 39547
			public static LocString ELEMENTTRANSITIONSFROM = "Transitions From";

			// Token: 0x04009A7C RID: 39548
			public static LocString ELEMENTCONSUMEDBY = "Applications";

			// Token: 0x04009A7D RID: 39549
			public static LocString ELEMENTPRODUCEDBY = "Produced By";

			// Token: 0x04009A7E RID: 39550
			public static LocString MATERIALUSEDTOCONSTRUCT = "Construction Uses";

			// Token: 0x04009A7F RID: 39551
			public static LocString SECTION_UNLOCKABLES = "Undiscovered Data";

			// Token: 0x04009A80 RID: 39552
			public static LocString CONTENTLOCKED = "Undiscovered";

			// Token: 0x04009A81 RID: 39553
			public static LocString CONTENTLOCKED_SUBTITLE = "More research or exploration is required";

			// Token: 0x04009A82 RID: 39554
			public static LocString INTERNALBATTERY = "Battery";

			// Token: 0x04009A83 RID: 39555
			public static LocString INTERNALSTORAGE = "Storage";

			// Token: 0x04009A84 RID: 39556
			public static LocString CRITTERMAXAGE = "Life Span";

			// Token: 0x04009A85 RID: 39557
			public static LocString CRITTEROVERCROWDING = "Space Required";

			// Token: 0x04009A86 RID: 39558
			public static LocString CRITTERDROPS = "Drops";

			// Token: 0x04009A87 RID: 39559
			public static LocString CRITTER_EXTRA_DIET_PRODUCTION = "Dewdrip";

			// Token: 0x04009A88 RID: 39560
			public static LocString FOODEFFECTS = "Nutritional Effects";

			// Token: 0x04009A89 RID: 39561
			public static LocString FOODSWITHEFFECT = "Foods with this effect";

			// Token: 0x04009A8A RID: 39562
			public static LocString EQUIPMENTEFFECTS = "Effects";
		}

		// Token: 0x020021A1 RID: 8609
		public class FORMAT_STRINGS
		{
			// Token: 0x04009A8B RID: 39563
			public static LocString TEMPERATURE_OVER = "Temperature over {0}";

			// Token: 0x04009A8C RID: 39564
			public static LocString TEMPERATURE_UNDER = "Temperature under {0}";

			// Token: 0x04009A8D RID: 39565
			public static LocString CONSTRUCTION_TIME = "Build Time: {0} seconds";

			// Token: 0x04009A8E RID: 39566
			public static LocString BUILDING_SIZE = "Building Size: {0} wide x {1} high";

			// Token: 0x04009A8F RID: 39567
			public static LocString MATERIAL_MASS = "{0} {1}";

			// Token: 0x04009A90 RID: 39568
			public static LocString TRANSITION_LABEL_TO_ONE_ELEMENT = "{0} to {1}";

			// Token: 0x04009A91 RID: 39569
			public static LocString TRANSITION_LABEL_TO_TWO_ELEMENTS = "{0} to {1} and {2}";
		}

		// Token: 0x020021A2 RID: 8610
		public class CREATURE_DESCRIPTORS
		{
			// Token: 0x04009A92 RID: 39570
			public static LocString MAXAGE = "This critter's typical " + UI.FormatAsLink("Life Span", "CREATURES::GUIDE::FERTILITY") + " is <b>{0} cycles</b>.";

			// Token: 0x04009A93 RID: 39571
			public static LocString OVERCROWDING = UI.FormatAsLink("Crowded", "CREATURES::GUIDE::MOOD") + " when a room has less than <b>{0} cells</b> of space for each critter.";

			// Token: 0x04009A94 RID: 39572
			public static LocString CONFINED = UI.FormatAsLink("Confined", "CREATURES::GUIDE::MOOD") + " when a room is smaller than <b>{0} cells</b>.";

			// Token: 0x04009A95 RID: 39573
			public static LocString NON_LETHAL_RANGE = "Livable range: <b>{0}</b> to <b>{1}</b>";

			// Token: 0x02002B91 RID: 11153
			public class TEMPERATURE
			{
				// Token: 0x0400BE90 RID: 48784
				public static LocString COMFORT_RANGE = "Comfort range: <b>{0}</b> to <b>{1}</b>";

				// Token: 0x0400BE91 RID: 48785
				public static LocString NON_LETHAL_RANGE = "Livable range: <b>{0}</b> to <b>{1}</b>";
			}
		}

		// Token: 0x020021A3 RID: 8611
		public class ROBOT_DESCRIPTORS
		{
			// Token: 0x02002B92 RID: 11154
			public class BATTERY
			{
				// Token: 0x0400BE92 RID: 48786
				public static LocString CAPACITY = "Battery capacity: <b>{0}" + UI.UNITSUFFIXES.ELECTRICAL.JOULE + "</b>";
			}

			// Token: 0x02002B93 RID: 11155
			public class STORAGE
			{
				// Token: 0x0400BE93 RID: 48787
				public static LocString CAPACITY = "Internal storage: <b>{0}" + UI.UNITSUFFIXES.MASS.KILOGRAM + "</b>";
			}
		}

		// Token: 0x020021A4 RID: 8612
		public class PAGENOTFOUND
		{
			// Token: 0x04009A96 RID: 39574
			public static LocString TITLE = "Data Not Found";

			// Token: 0x04009A97 RID: 39575
			public static LocString SUBTITLE = "This database entry is under construction or unavailable";

			// Token: 0x04009A98 RID: 39576
			public static LocString BODY = "";
		}

		// Token: 0x020021A5 RID: 8613
		public class CATEGORIES
		{
			// Token: 0x02002B94 RID: 11156
			public class SHARED
			{
				// Token: 0x0400BE94 RID: 48788
				public static LocString BUILDINGS_LIST_TITLE = "Buildings in this category:";
			}

			// Token: 0x02002B95 RID: 11157
			public class CREATURERELOCATOR
			{
				// Token: 0x0400BE95 RID: 48789
				public static LocString NAME = UI.FormatAsLink("Critter relocator", "GROUPCREATURERELOCATOR");

				// Token: 0x0400BE96 RID: 48790
				public static LocString TITLE = "Critter Relocators";

				// Token: 0x0400BE97 RID: 48791
				public static LocString DESCRIPTION = "Buildings that facilitate the movement of " + UI.FormatAsLink("Critters", "CREATURES") + " from one location to another.";

				// Token: 0x0400BE98 RID: 48792
				public static LocString FLAVOUR = "";
			}

			// Token: 0x02002B96 RID: 11158
			public class FARMBUILDING
			{
				// Token: 0x0400BE99 RID: 48793
				public static LocString NAME = UI.FormatAsLink("Farm Building", "GROUPFARMBUILDING");

				// Token: 0x0400BE9A RID: 48794
				public static LocString TITLE = "Farm Buildings";

				// Token: 0x0400BE9B RID: 48795
				public static LocString DESCRIPTION = "Buildings that Duplicants can use to plant and tend to a wide variety of colony-sustaining crops.";

				// Token: 0x0400BE9C RID: 48796
				public static LocString FLAVOUR = "";
			}

			// Token: 0x02002B97 RID: 11159
			public class BIONICBUILDING
			{
				// Token: 0x0400BE9D RID: 48797
				public static LocString NAME = UI.FormatAsLink("Bionic Service Station", "GROUPBIONICBUILDING");

				// Token: 0x0400BE9E RID: 48798
				public static LocString TITLE = "Bionic Service Stations";

				// Token: 0x0400BE9F RID: 48799
				public static LocString DESCRIPTION = "Buildings that keep Bionic Duplicants' complex inner machinery operating smoothly.";

				// Token: 0x0400BEA0 RID: 48800
				public static LocString FLAVOUR = "";
			}
		}

		// Token: 0x020021A6 RID: 8614
		public class ROOM_REQUIREMENT_CLASS
		{
			// Token: 0x04009A99 RID: 39577
			public static LocString NAME = UI.FormatAsLink("Category", "BUILDCATEGORYCATEGORY");

			// Token: 0x02002B98 RID: 11160
			public class SHARED
			{
				// Token: 0x0400BEA1 RID: 48801
				public static LocString BUILDINGS_LIST_TITLE = "Buildings in this category:";

				// Token: 0x0400BEA2 RID: 48802
				public static LocString ROOMS_REQUIRED_LIST_TITLE = "Required in:";

				// Token: 0x0400BEA3 RID: 48803
				public static LocString ROOMS_CONFLICT_LIST_TITLE = "Conflicts with:";
			}

			// Token: 0x02002B99 RID: 11161
			public class INDUSTRIALMACHINERY
			{
				// Token: 0x0400BEA4 RID: 48804
				public static LocString TITLE = "Industrial Machinery";

				// Token: 0x0400BEA5 RID: 48805
				public static LocString DESCRIPTION = "Buildings that generate power, manufacture equipment, refine resources, and provide other fundamental colony requirements.";

				// Token: 0x0400BEA6 RID: 48806
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEA7 RID: 48807
				public static LocString CONFLICTINGROOMS = string.Concat(new string[]
				{
					"    • ",
					UI.FormatAsLink("Latrine", "LATRINE"),
					"\n    • ",
					UI.FormatAsLink("Washroom", "PLUMBEDBATHROOM"),
					"\n    • ",
					UI.FormatAsLink("Barracks", "BARRACKS"),
					"\n    • ",
					UI.FormatAsLink("Luxury Barracks", "BEDROOM"),
					"\n    • ",
					UI.FormatAsLink("Private Bedroom", "PRIVATE BEDROOM"),
					"\n    • ",
					UI.FormatAsLink("Mess Hall", "MESSHALL"),
					"\n    • ",
					UI.FormatAsLink("Great Hall", "GREATHALL"),
					"\n    • ",
					UI.FormatAsLink("Massage Clinic", "MASSAGE_CLINIC"),
					"\n    • ",
					UI.FormatAsLink("Hospital", "HOSPITAL"),
					"\n    • ",
					UI.FormatAsLink("Laboratory", "LABORATORY"),
					"\n    • ",
					UI.FormatAsLink("Recreation Room", "REC_ROOM")
				});
			}

			// Token: 0x02002B9A RID: 11162
			public class RECBUILDING
			{
				// Token: 0x0400BEA8 RID: 48808
				public static LocString TITLE = "Recreational Buildings";

				// Token: 0x0400BEA9 RID: 48809
				public static LocString DESCRIPTION = "Buildings that provide essential support for fragile Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";

				// Token: 0x0400BEAA RID: 48810
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEAB RID: 48811
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Great Hall", "GREATHALL") + " \n    • " + UI.FormatAsLink("Recreation Room", "REC_ROOM");
			}

			// Token: 0x02002B9B RID: 11163
			public class CLINIC
			{
				// Token: 0x0400BEAC RID: 48812
				public static LocString TITLE = "Medical Equipment";

				// Token: 0x0400BEAD RID: 48813
				public static LocString DESCRIPTION = "Buildings designed to help sick Duplicants heal and minimize the spread of " + UI.FormatAsLink("Disease", "DISEASE") + ".";

				// Token: 0x0400BEAE RID: 48814
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEAF RID: 48815
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Hospital", "HOSPITAL");
			}

			// Token: 0x02002B9C RID: 11164
			public class WASHSTATION
			{
				// Token: 0x0400BEB0 RID: 48816
				public static LocString TITLE = "Wash Stations";

				// Token: 0x0400BEB1 RID: 48817
				public static LocString DESCRIPTION = "Buildings that remove " + UI.FormatAsLink("disease", "DISEASE") + "-spreading germs from Duplicant bodies. Not all wash stations require plumbing.";

				// Token: 0x0400BEB2 RID: 48818
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEB3 RID: 48819
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Latrine", "LATRINE");
			}

			// Token: 0x02002B9D RID: 11165
			public class ADVANCEDWASHSTATION
			{
				// Token: 0x0400BEB4 RID: 48820
				public static LocString TITLE = "Plumbed Wash Stations";

				// Token: 0x0400BEB5 RID: 48821
				public static LocString DESCRIPTION = "Buildings that require plumbing in order to remove " + UI.FormatAsLink("disease", "DISEASE") + "-spreading germs from Duplicant bodies.";

				// Token: 0x0400BEB6 RID: 48822
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEB7 RID: 48823
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Washroom", "PLUMBEDBATHROOM");
			}

			// Token: 0x02002B9E RID: 11166
			public class TOILETTYPE
			{
				// Token: 0x0400BEB8 RID: 48824
				public static LocString TITLE = "Toilets";

				// Token: 0x0400BEB9 RID: 48825
				public static LocString DESCRIPTION = "Buildings that give Duplicants a sanitary and dignified place to conduct essential \"business.\"";

				// Token: 0x0400BEBA RID: 48826
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEBB RID: 48827
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Latrine", "LATRINE") + "\n    • " + UI.FormatAsLink("Hospital", "HOSPITAL");
			}

			// Token: 0x02002B9F RID: 11167
			public class FLUSHTOILETTYPE
			{
				// Token: 0x0400BEBC RID: 48828
				public static LocString TITLE = UI.FormatAsLink("Flush Toilets", "FLUSHTOILETTYPE");

				// Token: 0x0400BEBD RID: 48829
				public static LocString DESCRIPTION = "Buildings that give Duplicants a sanitary and dignified place to conduct essential \"business\"...and then flush away the evidence.";

				// Token: 0x0400BEBE RID: 48830
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEBF RID: 48831
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Washroom", "PLUMBEDBATHROOM");
			}

			// Token: 0x02002BA0 RID: 11168
			public class SCIENCEBUILDING
			{
				// Token: 0x0400BEC0 RID: 48832
				public static LocString TITLE = "Science Buildings";

				// Token: 0x0400BEC1 RID: 48833
				public static LocString DESCRIPTION = "Buildings that allow Duplicants to learn about the world around them, and beyond.";

				// Token: 0x0400BEC2 RID: 48834
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEC3 RID: 48835
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Laboratory", "LABORATORY");
			}

			// Token: 0x02002BA1 RID: 11169
			public class DECORATION
			{
				// Token: 0x0400BEC4 RID: 48836
				public static LocString TITLE = "Decor Items";

				// Token: 0x0400BEC5 RID: 48837
				public static LocString DESCRIPTION = "Buildings that give the colony a valuable aesthetic boost, and allow Duplicants to express themselves creatively.\n\nSome rooms require Fancy Decor items, which contribute extra-high levels of aesthetic enhancement.";

				// Token: 0x0400BEC6 RID: 48838
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEC7 RID: 48839
				public static LocString ROOMSREQUIRING = string.Concat(new string[]
				{
					"    • ",
					UI.FormatAsLink("Luxury Barracks", "BEDROOM"),
					"\n    • ",
					UI.FormatAsLink("Private Bedroom", "PRIVATE BEDROOM"),
					"\n    • ",
					UI.FormatAsLink("Great Hall", "GREATHALL"),
					"\n    • ",
					UI.FormatAsLink("Massage Clinic", "MASSAGECLINIC"),
					"\n    • ",
					UI.FormatAsLink("Recreation Room", "REC_ROOM")
				});
			}

			// Token: 0x02002BA2 RID: 11170
			public class RANCHSTATIONTYPE
			{
				// Token: 0x0400BEC8 RID: 48840
				public static LocString TITLE = "Ranching Buildings";

				// Token: 0x0400BEC9 RID: 48841
				public static LocString DESCRIPTION = "Buildings dedicated to " + UI.FormatAsLink("Critter", "CREATURES") + " husbandry.";

				// Token: 0x0400BECA RID: 48842
				public static LocString FLAVOUR = "";

				// Token: 0x0400BECB RID: 48843
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Stable", "CREATUREPEN");
			}

			// Token: 0x02002BA3 RID: 11171
			public class BEDTYPE
			{
				// Token: 0x0400BECC RID: 48844
				public static LocString TITLE = "Beds";

				// Token: 0x0400BECD RID: 48845
				public static LocString DESCRIPTION = "Buildings that allow Duplicants to get much-needed rest. If a Duplicant is not assigned one, they will sleep on the floor.";

				// Token: 0x0400BECE RID: 48846
				public static LocString FLAVOUR = "";

				// Token: 0x0400BECF RID: 48847
				public static LocString CONFLICTINGROOMS = string.Concat(new string[]
				{
					"    • ",
					UI.FormatAsLink("Luxury Barracks", "BEDROOM"),
					" (No ",
					UI.FormatAsLink("Cots", "BED"),
					")\n    • ",
					UI.FormatAsLink("Private Bedroom", "PRIVATE BEDROOM"),
					" (No ",
					UI.FormatAsLink("Cots", "BED"),
					")"
				});

				// Token: 0x0400BED0 RID: 48848
				public static LocString ROOMSREQUIRING = string.Concat(new string[]
				{
					"    • ",
					UI.FormatAsLink("Barracks", "BARRACKS"),
					"\n    • ",
					UI.FormatAsLink("Luxury Barracks", "BEDROOM"),
					" (one or more ",
					UI.FormatAsLink("Comfy Beds", "LUXURYBED"),
					")\n    • ",
					UI.FormatAsLink("Private Bedroom", "PRIVATE BEDROOM"),
					" (single ",
					UI.FormatAsLink("Comfy Bed", "LUXURYBED"),
					")"
				});
			}

			// Token: 0x02002BA4 RID: 11172
			public class LIGHTSOURCE
			{
				// Token: 0x0400BED1 RID: 48849
				public static LocString TITLE = "Light Sources";

				// Token: 0x0400BED2 RID: 48850
				public static LocString DESCRIPTION = "Buildings that produce light, either by design or as a result of their primary operations.";

				// Token: 0x0400BED3 RID: 48851
				public static LocString FLAVOUR = "";

				// Token: 0x0400BED4 RID: 48852
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Laboratory", "LABORATORY");
			}

			// Token: 0x02002BA5 RID: 11173
			public class ROCKETINTERIOR
			{
				// Token: 0x0400BED5 RID: 48853
				public static LocString TITLE = "Rocket Interior";

				// Token: 0x0400BED6 RID: 48854
				public static LocString DESCRIPTION = "Buildings that must be built inside a rocket.";

				// Token: 0x0400BED7 RID: 48855
				public static LocString FLAVOUR = "";
			}

			// Token: 0x02002BA6 RID: 11174
			public class COOKTOP
			{
				// Token: 0x0400BED8 RID: 48856
				public static LocString TITLE = "Cooking Stations";

				// Token: 0x0400BED9 RID: 48857
				public static LocString DESCRIPTION = "Buildings that transform individual ingredients into delicious meals.";

				// Token: 0x0400BEDA RID: 48858
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEDB RID: 48859
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Kitchen", "KITCHEN");
			}

			// Token: 0x02002BA7 RID: 11175
			public class WARMINGSTATION
			{
				// Token: 0x0400BEDC RID: 48860
				public static LocString TITLE = "Warming Stations";

				// Token: 0x0400BEDD RID: 48861
				public static LocString DESCRIPTION = "Buildings that Duplicants will visit when they are suffering the effects of cold environments.";

				// Token: 0x0400BEDE RID: 48862
				public static LocString FLAVOUR = "";
			}

			// Token: 0x02002BA8 RID: 11176
			public class GENERATORTYPE
			{
				// Token: 0x0400BEDF RID: 48863
				public static LocString TITLE = "Generators";

				// Token: 0x0400BEE0 RID: 48864
				public static LocString DESCRIPTION = "Buildings that generate the " + UI.FormatAsLink("Power", "POWER") + " required to run machinery in my colony.\n\nBasic requirements can be met with an entry-level generator, but heavier-duty buildings are essential to colony development.";

				// Token: 0x0400BEE1 RID: 48865
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEE2 RID: 48866
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Power Plant", "POWERPLANT");
			}

			// Token: 0x02002BA9 RID: 11177
			public class POWERBUILDING
			{
				// Token: 0x0400BEE3 RID: 48867
				public static LocString TITLE = "Power Buildings";

				// Token: 0x0400BEE4 RID: 48868
				public static LocString DESCRIPTION = "Buildings that generate, manage or store the electrical power a colony needs to thrive and expand.";

				// Token: 0x0400BEE5 RID: 48869
				public static LocString FLAVOUR = "";

				// Token: 0x0400BEE6 RID: 48870
				public static LocString ROOMSREQUIRING = "    • " + UI.FormatAsLink("Power Plant", "POWERPLANT");
			}
		}

		// Token: 0x020021A7 RID: 8615
		public class BEETA
		{
			// Token: 0x04009A9A RID: 39578
			public static LocString TITLE = "Beeta";

			// Token: 0x04009A9B RID: 39579
			public static LocString SUBTITLE = "Aggressive Critter";

			// Token: 0x02002BAA RID: 11178
			public class BODY
			{
				// Token: 0x0400BEE7 RID: 48871
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"Beetas are insectoid creatures that enjoy a symbiotic relationship with the radioactive environment they thrive in.\n\nMuch like the honey bee gathers nectar and processes it to honey, the Beeta turns ",
					UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
					" into ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					" through a complex process of isotope separation inside the Beeta Hive.\n\nWhen first observing the Beeta's enrichment process, many scientists note with surprise just how much more efficient the cooperative combination of insect and hive is when compared to even the most advanced industrial processes."
				});
			}
		}

		// Token: 0x020021A8 RID: 8616
		public class BUTTERFLY
		{
			// Token: 0x04009A9C RID: 39580
			public static LocString SPECIES_TITLE = "Mimikas";

			// Token: 0x04009A9D RID: 39581
			public static LocString SPECIES_SUBTITLE = "Uncategorized Organism";

			// Token: 0x04009A9E RID: 39582
			public static LocString TITLE = "Mimika";

			// Token: 0x04009A9F RID: 39583
			public static LocString SUBTITLE = "Critter?";

			// Token: 0x02002BAB RID: 11179
			public class BODY
			{
				// Token: 0x0400BEE8 RID: 48872
				public static LocString CONTAINER1 = "Mimikas are difficult to categorize. Biologists theorize that the " + UI.FormatAsLink("Mimika Bud", "BUTTERFLYPLANT") + " engages in this rare variation of reproductive mimicry to improve seed-dispersal and survive the extinction of key species native to its original habitat.\n\nThese charming moth-like organisms feature a microscopic cluster of phytoprotein \"brain\" cells and are driven by a singular instinct to tend to their host plants.\n\nDue to a monogenic malfunction, however, this plant-tending behavior benefits all of the flora in its area <i>except</i> its own.";
			}
		}

		// Token: 0x020021A9 RID: 8617
		public class CHAMELEON
		{
			// Token: 0x04009AA0 RID: 39584
			public static LocString SPECIES_TITLE = "Dartles";

			// Token: 0x04009AA1 RID: 39585
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AA2 RID: 39586
			public static LocString TITLE = "Dartle";

			// Token: 0x04009AA3 RID: 39587
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BAC RID: 11180
			public class BODY
			{
				// Token: 0x0400BEE9 RID: 48873
				public static LocString CONTAINER1 = "Dartles are docile reptilian critters whose existence centers around maximum energy conservation.\n\nThis species was once known as the fastest land-based critter in existence. However, its deep aversion to physical exertion has grown stronger than its desire to escape predation. Researchers are uncertain what, if anything, the Dartle is saving its energy for.";
			}
		}

		// Token: 0x020021AA RID: 8618
		public class DIVERGENT
		{
			// Token: 0x04009AA4 RID: 39588
			public static LocString TITLE = "Divergent";

			// Token: 0x04009AA5 RID: 39589
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BAD RID: 11181
			public class BODY
			{
				// Token: 0x0400BEEA RID: 48874
				public static LocString CONTAINER1 = "'Divergent' is the name given to the two different genders of one species, the Sweetle and the Grubgrub, both of which are able to reproduce asexually and tend to Grubfruit Plants.\n\nWhen tending to the Grubfruit Plant, both gender variants of the Divergent display the exact same behaviors, however the Grubgrub possesses slightly more tiny facial hair which helps in pollinating the plants and stimulates faster growth.";
			}
		}

		// Token: 0x020021AB RID: 8619
		public class DRECKO
		{
			// Token: 0x04009AA6 RID: 39590
			public static LocString SPECIES_TITLE = "Dreckos";

			// Token: 0x04009AA7 RID: 39591
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AA8 RID: 39592
			public static LocString TITLE = "Drecko";

			// Token: 0x04009AA9 RID: 39593
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BAE RID: 11182
			public class BODY
			{
				// Token: 0x0400BEEB RID: 48875
				public static LocString CONTAINER1 = "Dreckos are a reptilian species boasting billions of microscopic hairs on their feet, allowing them to stick to and climb most surfaces.";

				// Token: 0x0400BEEC RID: 48876
				public static LocString CONTAINER2 = "The tail of the Drecko, called the \"train\", is purely for decoration and can be lost or shorn without harm to the animal.\n\nAs a result, Drecko fibers are often farmed for use in textile production.\n\nCaring for Dreckos is a fulfilling endeavor thanks to their companionable personalities.\n\nSome domestic Dreckos have even been known to respond to their own names.";
			}
		}

		// Token: 0x020021AC RID: 8620
		public class GLOSSY
		{
			// Token: 0x04009AAA RID: 39594
			public static LocString TITLE = "Glossy Drecko";

			// Token: 0x04009AAB RID: 39595
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BAF RID: 11183
			public class BODY
			{
				// Token: 0x0400BEED RID: 48877
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Glossy\" Drecko variant</smallcaps>";
			}
		}

		// Token: 0x020021AD RID: 8621
		public class FETCHDRONE
		{
			// Token: 0x04009AAC RID: 39596
			public static LocString TITLE = "Flydo";

			// Token: 0x04009AAD RID: 39597
			public static LocString SUBTITLE = "Delivery Robot";

			// Token: 0x02002BB0 RID: 11184
			public class BODY
			{
				// Token: 0x0400BEEE RID: 48878
				public static LocString CONTAINER1 = "The Flydo is an airborne delivery robot designed to transport solid items across great distances, both horizontal and vertical.\n\nThese wireless robots may deplete their " + UI.FormatAsLink("Power Banks", "ELECTROBANK") + " in mid-flight and temporarily shut down until a replacement is delivered. Once rebooted, they reawaken feeling as energized as if it was their very first day on the job.\n\nTragically, some powered-down Flydos fall into liquid pools and may never be rebooted at all.";
			}
		}

		// Token: 0x020021AE RID: 8622
		public class GASSYMOO
		{
			// Token: 0x04009AAE RID: 39598
			public static LocString TITLE = "Gassy Moo";

			// Token: 0x04009AAF RID: 39599
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BB1 RID: 11185
			public class BODY
			{
				// Token: 0x0400BEEF RID: 48879
				public static LocString CONTAINER1 = "Little is currently known of the Gassy Moo due to its alien nature and origin.\n\nIt is capable of surviving in zero gravity conditions and no atmosphere, and is dependent on a second alien species, " + UI.FormatAsLink("Gas Grass", "GASGRASS") + ", for its sustenance and survival.";

				// Token: 0x0400BEF0 RID: 48880
				public static LocString CONTAINER2 = "The Moo has an even temperament and can be farmed for Natural Gas, though their method of reproduction has been as of yet undiscovered.";
			}
		}

		// Token: 0x020021AF RID: 8623
		public class HATCH
		{
			// Token: 0x04009AB0 RID: 39600
			public static LocString SPECIES_TITLE = "Hatches";

			// Token: 0x04009AB1 RID: 39601
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AB2 RID: 39602
			public static LocString TITLE = "Hatch";

			// Token: 0x04009AB3 RID: 39603
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BB2 RID: 11186
			public class BODY
			{
				// Token: 0x0400BEF1 RID: 48881
				public static LocString CONTAINER1 = "The Hatch has no eyes and is completely blind, although a photosensitive patch atop its head is capable of detecting even minor changes in overhead light, making it prefer dark caves and tunnels.";
			}
		}

		// Token: 0x020021B0 RID: 8624
		public class STONE
		{
			// Token: 0x04009AB4 RID: 39604
			public static LocString TITLE = "Stone Hatch";

			// Token: 0x04009AB5 RID: 39605
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BB3 RID: 11187
			public class BODY
			{
				// Token: 0x0400BEF2 RID: 48882
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Stone\" Hatch variant</smallcaps>";

				// Token: 0x0400BEF3 RID: 48883
				public static LocString CONTAINER2 = "When attempting to pet a Hatch, inexperienced handlers make the mistake of reaching out too quickly for the creature's head.\n\nThis triggers a fear response in the Hatch, as its photosensitive patch of skin called the \"parietal eye\" interprets this sudden light change as an incoming aerial predator.";
			}
		}

		// Token: 0x020021B1 RID: 8625
		public class SAGE
		{
			// Token: 0x04009AB6 RID: 39606
			public static LocString TITLE = "Sage Hatch";

			// Token: 0x04009AB7 RID: 39607
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BB4 RID: 11188
			public class BODY
			{
				// Token: 0x0400BEF4 RID: 48884
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Sage\" Hatch variant</smallcaps>";

				// Token: 0x0400BEF5 RID: 48885
				public static LocString CONTAINER2 = "It is difficult to classify the Hatch's diet as the term \"omnivore\" does not extend to the non-organic materials it is capable of ingesting.\n\nA more appropriate term is \"totumvore\", given that it can consume and find nutritional value in nearly every known substance.";
			}
		}

		// Token: 0x020021B2 RID: 8626
		public class SMOOTH
		{
			// Token: 0x04009AB8 RID: 39608
			public static LocString TITLE = "Smooth Hatch";

			// Token: 0x04009AB9 RID: 39609
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BB5 RID: 11189
			public class BODY
			{
				// Token: 0x0400BEF6 RID: 48886
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Smooth\" Hatch variant</smallcaps>";

				// Token: 0x0400BEF7 RID: 48887
				public static LocString CONTAINER2 = "The proper way to pet a Hatch is to touch any of its four feet to first make it aware of your presence, then either scratch the soft segmented underbelly or firmly pat the creature's thick chitinous back.";
			}
		}

		// Token: 0x020021B3 RID: 8627
		public class ICEBELLY
		{
			// Token: 0x04009ABA RID: 39610
			public static LocString SPECIES_TITLE = "Bammoths";

			// Token: 0x04009ABB RID: 39611
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009ABC RID: 39612
			public static LocString TITLE = "Bammoth";

			// Token: 0x04009ABD RID: 39613
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BB6 RID: 11190
			public class BODY
			{
				// Token: 0x0400BEF8 RID: 48888
				public static LocString CONTAINER1 = "The Bammoth is one of the oldest species on record, with ancient skeletal remains dating back approximately 10,000 years.\n\nThis placid herbivore is known for its unique body language: an angry young Bammoth expresses displeasure by flopping down dramatically in front of its opponent, while older creatures with limited mobility will sit facing away from the source of their annoyance.\n\nLicking the ground in front of a caregiver can be a sign of either deep affection or mineral deficiency.";
			}
		}

		// Token: 0x020021B4 RID: 8628
		public class MOLE
		{
			// Token: 0x04009ABE RID: 39614
			public static LocString TITLE = "Shove Vole";

			// Token: 0x04009ABF RID: 39615
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BB7 RID: 11191
			public class BODY
			{
				// Token: 0x0400BEF9 RID: 48889
				public static LocString CONTAINER1 = "The Shove Vole is a unique creature that possesses two fully developed sets of lungs, allowing it to hold its breath during the long periods it spends underground.";

				// Token: 0x0400BEFA RID: 48890
				public static LocString CONTAINER2 = "Drill-shaped keratin structures circling the Vole's body aids its ability to drill at high speeds through most natural materials.";
			}
		}

		// Token: 0x020021B5 RID: 8629
		public class VARIANT_DELICACY
		{
			// Token: 0x04009AC0 RID: 39616
			public static LocString TITLE = "Delecta Vole";

			// Token: 0x04009AC1 RID: 39617
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BB8 RID: 11192
			public class BODY
			{
				// Token: 0x0400BEFB RID: 48891
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Delecta\" Vole variant</smallcaps>";
			}
		}

		// Token: 0x020021B6 RID: 8630
		public class MORB
		{
			// Token: 0x04009AC2 RID: 39618
			public static LocString TITLE = "Morb";

			// Token: 0x04009AC3 RID: 39619
			public static LocString SUBTITLE = "Pest Critter";

			// Token: 0x02002BB9 RID: 11193
			public class BODY
			{
				// Token: 0x0400BEFC RID: 48892
				public static LocString CONTAINER1 = "The Morb is a versatile scavenger, capable of breaking down and consuming dead matter from most plant and animal species.";

				// Token: 0x0400BEFD RID: 48893
				public static LocString CONTAINER2 = "It poses a severe disease risk to humans due to the thick slime it excretes to surround its inner cartilage structures.\n\nA single teaspoon of Morb slime can contain up to a quadrillion bacteria that work to deter would-be predators and liquefy its food.";

				// Token: 0x0400BEFE RID: 48894
				public static LocString CONTAINER3 = "Petting a Morb is not recommended.";
			}
		}

		// Token: 0x020021B7 RID: 8631
		public class MOSQUITO
		{
			// Token: 0x04009AC4 RID: 39620
			public static LocString SPECIES_TITLE = "Gnits";

			// Token: 0x04009AC5 RID: 39621
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AC6 RID: 39622
			public static LocString TITLE = "Gnit";

			// Token: 0x04009AC7 RID: 39623
			public static LocString SUBTITLE = "Pest Critter";

			// Token: 0x02002BBA RID: 11194
			public class BODY
			{
				// Token: 0x0400BEFF RID: 48895
				public static LocString CONTAINER1 = "The Gnit is an insectoid critter that relies on supplementary nutrients and minerals from Duplicants and other critters to mitigate the outsized energy requirements of its reproductive system.\n\nGnits' antennae double as maxillary palps equipped with finely tuned olfactory receptor neurons that enable them to pinpoint nutrient sources from a great distance.\n\nThese same receptors activate the insectoid's flight response upon detecting certain changes in an organism's respiration, such as the sharp intake of breath that precedes a fatal counter-attack.";
			}
		}

		// Token: 0x020021B8 RID: 8632
		public class PACU
		{
			// Token: 0x04009AC8 RID: 39624
			public static LocString SPECIES_TITLE = "Pacus";

			// Token: 0x04009AC9 RID: 39625
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009ACA RID: 39626
			public static LocString TITLE = "Pacu";

			// Token: 0x04009ACB RID: 39627
			public static LocString SUBTITLE = "Aquatic Critter";

			// Token: 0x02002BBB RID: 11195
			public class BODY
			{
				// Token: 0x0400BF00 RID: 48896
				public static LocString CONTAINER1 = "The Pacu fish is often interpreted as possessing a vacant stare due to its large and unblinking eyes, yet they are remarkably bright and friendly creatures.";
			}
		}

		// Token: 0x020021B9 RID: 8633
		public class TROPICAL
		{
			// Token: 0x04009ACC RID: 39628
			public static LocString TITLE = "Tropical Pacu";

			// Token: 0x04009ACD RID: 39629
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BBC RID: 11196
			public class BODY
			{
				// Token: 0x0400BF01 RID: 48897
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Tropical\" Pacu variant</smallcaps>";

				// Token: 0x0400BF02 RID: 48898
				public static LocString CONTAINER2 = "It is said that the average Pacu intelligence is comparable to that of a dog, and that they are capable of learning and distinguishing from over twenty human faces.";
			}
		}

		// Token: 0x020021BA RID: 8634
		public class CLEANER
		{
			// Token: 0x04009ACE RID: 39630
			public static LocString TITLE = "Gulp Fish";

			// Token: 0x04009ACF RID: 39631
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BBD RID: 11197
			public class BODY
			{
				// Token: 0x0400BF03 RID: 48899
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Gulp Fish\" Pacu variant</smallcaps>";

				// Token: 0x0400BF04 RID: 48900
				public static LocString CONTAINER2 = "Despite descending from the Pacu, the Gulp Fish is unique enough both in genetics and behavior to be considered its own subspecies.";
			}
		}

		// Token: 0x020021BB RID: 8635
		public class PIP
		{
			// Token: 0x04009AD0 RID: 39632
			public static LocString SPECIES_TITLE = "Pips";

			// Token: 0x04009AD1 RID: 39633
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AD2 RID: 39634
			public static LocString TITLE = "Pip";

			// Token: 0x04009AD3 RID: 39635
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BBE RID: 11198
			public class BODY
			{
				// Token: 0x0400BF05 RID: 48901
				public static LocString CONTAINER1 = "Pips are a member of the Rodentia order with a strong caching instinct that causes them to find and bury small objects, most often seeds.";

				// Token: 0x0400BF06 RID: 48902
				public static LocString CONTAINER2 = "It is unknown whether their caching behavior is a compulsion or a form of entertainment, as the Pip relies primarily on bark and wood for its survival.";

				// Token: 0x0400BF07 RID: 48903
				public static LocString CONTAINER3 = "Although the Pip lacks truly opposable thumbs, it nonetheless has highly dexterous paws that allow it to rummage through most tight to reach spaces in search of seeds and other treasures.";
			}
		}

		// Token: 0x020021BC RID: 8636
		public class VARIANT_HUG
		{
			// Token: 0x04009AD4 RID: 39636
			public static LocString TITLE = "Cuddle Pip";

			// Token: 0x04009AD5 RID: 39637
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BBF RID: 11199
			public class BODY
			{
				// Token: 0x0400BF08 RID: 48904
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Cuddle\" Pip variant</smallcaps>";

				// Token: 0x0400BF09 RID: 48905
				public static LocString CONTAINER2 = "Cuddle Pips are genetically predisposed to feel deeply affectionate towards the unhatched young of all species, and can often be observed hugging eggs.";
			}
		}

		// Token: 0x020021BD RID: 8637
		public class PLUGSLUG
		{
			// Token: 0x04009AD6 RID: 39638
			public static LocString TITLE = "Plug Slug";

			// Token: 0x04009AD7 RID: 39639
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BC0 RID: 11200
			public class BODY
			{
				// Token: 0x0400BF0A RID: 48906
				public static LocString CONTAINER1 = "Plug Slugs are fuzzy gastropoda that are able to cling to walls and ceilings thanks to an extreme triboelectric effect caused by friction between their fur and various surfaces.\n\nThis same phenomomen allows the Plug Slug to generate a significant amount of static electricity that can be converted into power.\n\nThe increased amount of static electricity a Plug Slug can generate when domesticated is due to the internal vibration, or contented 'humming', they demonstrate when all their needs are met.";
			}
		}

		// Token: 0x020021BE RID: 8638
		public class VARIANT_LIQUID
		{
			// Token: 0x04009AD8 RID: 39640
			public static LocString TITLE = "Sponge Slug";

			// Token: 0x04009AD9 RID: 39641
			public static LocString SUBTITLE = "Critter Morph";
		}

		// Token: 0x020021BF RID: 8639
		public class VARIANT_GAS
		{
			// Token: 0x04009ADA RID: 39642
			public static LocString TITLE = "Smog Slug";

			// Token: 0x04009ADB RID: 39643
			public static LocString SUBTITLE = "Critter Morph";
		}

		// Token: 0x020021C0 RID: 8640
		public class POKESHELL
		{
			// Token: 0x04009ADC RID: 39644
			public static LocString SPECIES_TITLE = "Pokeshells";

			// Token: 0x04009ADD RID: 39645
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009ADE RID: 39646
			public static LocString TITLE = "Pokeshell";

			// Token: 0x04009ADF RID: 39647
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BC1 RID: 11201
			public class BODY
			{
				// Token: 0x0400BF0B RID: 48907
				public static LocString CONTAINER1 = "Pokeshells are bottom-feeding invertebrates that consume the waste and discarded food left behind by other creatures.";

				// Token: 0x0400BF0C RID: 48908
				public static LocString CONTAINER2 = "They have formidably sized claws that fold safely into their shells for protection when not in use.";

				// Token: 0x0400BF0D RID: 48909
				public static LocString CONTAINER3 = "As Pokeshells mature they must periodically shed portions of their exoskeletons to make room for new growth.";

				// Token: 0x0400BF0E RID: 48910
				public static LocString CONTAINER4 = "Although the most dramatic sheds occur early in a Pokeshell's adolescence, they will continue growing and shedding throughout their adult lives, until the day they eventually die.";
			}
		}

		// Token: 0x020021C1 RID: 8641
		public class VARIANT_WOOD
		{
			// Token: 0x04009AE0 RID: 39648
			public static LocString TITLE = "Oakshell";

			// Token: 0x04009AE1 RID: 39649
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BC2 RID: 11202
			public class BODY
			{
				// Token: 0x0400BF0F RID: 48911
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Oakshell\" variant</smallcaps>";
			}
		}

		// Token: 0x020021C2 RID: 8642
		public class VARIANT_FRESH_WATER
		{
			// Token: 0x04009AE2 RID: 39650
			public static LocString TITLE = "Sanishell";

			// Token: 0x04009AE3 RID: 39651
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BC3 RID: 11203
			public class BODY
			{
				// Token: 0x0400BF10 RID: 48912
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Sanishell\" variant</smallcaps>";
			}
		}

		// Token: 0x020021C3 RID: 8643
		public class PREHISTORICPACU
		{
			// Token: 0x04009AE4 RID: 39652
			public static LocString SPECIES_TITLE = "Jawbos";

			// Token: 0x04009AE5 RID: 39653
			public static LocString SPECIES_SUBTITLE = "Aquatic Species";

			// Token: 0x04009AE6 RID: 39654
			public static LocString TITLE = "Jawbo";

			// Token: 0x04009AE7 RID: 39655
			public static LocString SUBTITLE = "Aquatic Critter";

			// Token: 0x02002BC4 RID: 11204
			public class BODY
			{
				// Token: 0x0400BF11 RID: 48913
				public static LocString CONTAINER1 = "While the Jawbo may be terrifying to behold, in truth it is quite harmless for trained handlers.\n\nA disproportionately sized mandible makes it impossible for this critter to properly chew its food, necessitating a preference for prey that are small enough to be swallowed whole.\n\nThough much of its DNA suggests that the Jawbo evolved in isolation, it does share a small number of genetic markers with serrasalmids such as the " + UI.FormatAsLink("Pacu", "PACU") + ". Familial connection, however, does not preclude consideration as a food source.";
			}
		}

		// Token: 0x020021C4 RID: 8644
		public class PUFT
		{
			// Token: 0x04009AE8 RID: 39656
			public static LocString SPECIES_TITLE = "Pufts";

			// Token: 0x04009AE9 RID: 39657
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AEA RID: 39658
			public static LocString TITLE = "Puft";

			// Token: 0x04009AEB RID: 39659
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BC5 RID: 11205
			public class BODY
			{
				// Token: 0x0400BF12 RID: 48914
				public static LocString CONTAINER1 = "The Puft is a mellow creature whose limited brainpower is largely dedicated to sustaining its basic life processes.";
			}
		}

		// Token: 0x020021C5 RID: 8645
		public class SQUEAKY
		{
			// Token: 0x04009AEC RID: 39660
			public static LocString TITLE = "Squeaky Puft";

			// Token: 0x04009AED RID: 39661
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BC6 RID: 11206
			public class BODY
			{
				// Token: 0x0400BF13 RID: 48915
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Squeaky\" Puft variant</smallcaps>";

				// Token: 0x0400BF14 RID: 48916
				public static LocString CONTAINER2 = "Pufts often have a collection of asymmetric teeth lining the ridge of their mouths, although this feature is entirely vestigial as Pufts do not consume solid food.\n\nInstead, a baleen-like mesh of keratin at the back of the Puft's throat works to filter out tiny organisms and food particles from the air.\n\nUnusable air is expelled back out the Puft's posterior trunk, along with waste material and any indigestible particles or pathogens which it then evacuates as solid biomass.";
			}
		}

		// Token: 0x020021C6 RID: 8646
		public class DENSE
		{
			// Token: 0x04009AEE RID: 39662
			public static LocString TITLE = "Dense Puft";

			// Token: 0x04009AEF RID: 39663
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BC7 RID: 11207
			public class BODY
			{
				// Token: 0x0400BF15 RID: 48917
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Dense\" Puft variant</smallcaps>";

				// Token: 0x0400BF16 RID: 48918
				public static LocString CONTAINER2 = "The Puft is an easy creature to raise for first time handlers given its wholly amiable disposition and suggestible nature.\n\nIt is unusually tolerant of human handling and will allow itself to be patted or scratched nearly anywhere on its fuzzy body, including, unnervingly, directly on any of its three eyeballs.";
			}
		}

		// Token: 0x020021C7 RID: 8647
		public class PRINCE
		{
			// Token: 0x04009AF0 RID: 39664
			public static LocString TITLE = "Puft Prince";

			// Token: 0x04009AF1 RID: 39665
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BC8 RID: 11208
			public class BODY
			{
				// Token: 0x0400BF17 RID: 48919
				public static LocString CONTAINER1 = "<smallcaps>Pictured: Puft \"Prince\" variant</smallcaps>";

				// Token: 0x0400BF18 RID: 48920
				public static LocString CONTAINER2 = "A specialized air bladder in the Puft's chest cavity stores varying concentrations of gas, allowing it to control its buoyancy and float effortlessly through the air.\n\nCombined with extremely lightweight and elastic skin, the Puft is capable of maintaining flotation indefinitely with negligible energy expenditure. Its orientation and balance, meanwhile, are maintained by counterweighted formations of bone located in its otherwise useless legs.";
			}
		}

		// Token: 0x020021C8 RID: 8648
		public class RAPTOR
		{
			// Token: 0x04009AF2 RID: 39666
			public static LocString SPECIES_TITLE = "Rhexes";

			// Token: 0x04009AF3 RID: 39667
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AF4 RID: 39668
			public static LocString TITLE = "Rhex";

			// Token: 0x04009AF5 RID: 39669
			public static LocString SUBTITLE = "Carnivorous Critter";

			// Token: 0x02002BC9 RID: 11209
			public class BODY
			{
				// Token: 0x0400BF19 RID: 48921
				public static LocString CONTAINER1 = "For all their bluster, Rhexes are emotionally sensitive creatures that thrive on positive reinforcement, especially words of affirmation.\n\nThese sightless apex predators have exceptionally acidic gastric juices that kill most known bacteria and toxins, enabling them to safely digest their prey. This protective mechanism renders them susceptible to chronic ulcers if food is not readily available.\n\nThe Rhex's tailfeather can be shorn to harvest fibers for textile production. Rhexes don't mind this - some even enjoy it.";
			}
		}

		// Token: 0x020021C9 RID: 8649
		public class ROVER
		{
			// Token: 0x04009AF6 RID: 39670
			public static LocString TITLE = "Rover";

			// Token: 0x04009AF7 RID: 39671
			public static LocString SUBTITLE = "Scouting Robot";

			// Token: 0x02002BCA RID: 11210
			public class BODY
			{
				// Token: 0x0400BF1A RID: 48922
				public static LocString CONTAINER1 = "The Rover is a planetary scout robot programmed to land on and mine Planetoids where sending a Duplicant would put them unneccessarily in danger.\n\nRovers are programmed to be very pleasant and social when interacting with other beings. However, an unintended consequence of this programming is that the socialized robots tended to experience the same work slow-downs due to loneliness and low morale.\n\nTo compensate for this, the Rover was programmed to have two distinct personalities it can switch between to have pleasant in-depth conversations with itself during long stints alone.";
			}
		}

		// Token: 0x020021CA RID: 8650
		public class SEAL
		{
			// Token: 0x04009AF8 RID: 39672
			public static LocString SPECIES_TITLE = "Spigot Seals";

			// Token: 0x04009AF9 RID: 39673
			public static LocString SPECIES_SUBTITLE = "Domesticable Species";

			// Token: 0x04009AFA RID: 39674
			public static LocString TITLE = "Spigot Seal";

			// Token: 0x04009AFB RID: 39675
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BCB RID: 11211
			public class BODY
			{
				// Token: 0x0400BF1B RID: 48923
				public static LocString CONTAINER1 = "Spigot Seals are named for the hollow, cone-shaped glabellar protrusion that allows them to siphon nourishment directly from plants into the digestive sac located at the cone's base.\n\nIn order to draw nutritious fluids through this \"straw,\" the Spigot Seal compresses its nasal cavity and pumps its tongue up into its soft palate repeatedly, creating a vacuum.\n\nMealtimes are concluded by lapping at the air to reopen the airways and prevent accidental asphyxiation.\n\nMany handlers enjoy teaching this critter to clap its flippers, only to discover that there is no reliable method of limiting how often or how loudly the behavior is repeated.";
			}
		}

		// Token: 0x020021CB RID: 8651
		public class SHINEBUG
		{
			// Token: 0x04009AFC RID: 39676
			public static LocString SPECIES_TITLE = "Shine Bugs";

			// Token: 0x04009AFD RID: 39677
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009AFE RID: 39678
			public static LocString TITLE = "Shine Bug";

			// Token: 0x04009AFF RID: 39679
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BCC RID: 11212
			public class BODY
			{
				// Token: 0x0400BF1C RID: 48924
				public static LocString CONTAINER1 = "The bioluminescence of the Shine Bug's body serves the social purpose of finding and communicating with others of its kind.";
			}
		}

		// Token: 0x020021CC RID: 8652
		public class NEGA
		{
			// Token: 0x04009B00 RID: 39680
			public static LocString TITLE = "Abyss Bug";

			// Token: 0x04009B01 RID: 39681
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BCD RID: 11213
			public class BODY
			{
				// Token: 0x0400BF1D RID: 48925
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Abyss\" Shine Bug variant</smallcaps>";

				// Token: 0x0400BF1E RID: 48926
				public static LocString CONTAINER2 = "The Abyss Shine Bug morph has an unusual genetic mutation causing it to absorb light rather than emit it.";
			}
		}

		// Token: 0x020021CD RID: 8653
		public class CRYSTAL
		{
			// Token: 0x04009B02 RID: 39682
			public static LocString TITLE = "Radiant Bug";

			// Token: 0x04009B03 RID: 39683
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BCE RID: 11214
			public class BODY
			{
				// Token: 0x0400BF1F RID: 48927
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Radiant\" Shine Bug variant</smallcaps>";
			}
		}

		// Token: 0x020021CE RID: 8654
		public class SUNNY
		{
			// Token: 0x04009B04 RID: 39684
			public static LocString TITLE = "Sun Bug";

			// Token: 0x04009B05 RID: 39685
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BCF RID: 11215
			public class BODY
			{
				// Token: 0x0400BF20 RID: 48928
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Sun\" Shine Bug variant</smallcaps>";

				// Token: 0x0400BF21 RID: 48929
				public static LocString CONTAINER2 = "It is not uncommon for Shine Bugs to mistakenly approach inanimate sources of light in search of a friend.";
			}
		}

		// Token: 0x020021CF RID: 8655
		public class PLACID
		{
			// Token: 0x04009B06 RID: 39686
			public static LocString TITLE = "Azure Bug";

			// Token: 0x04009B07 RID: 39687
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BD0 RID: 11216
			public class BODY
			{
				// Token: 0x0400BF22 RID: 48930
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Azure\" Shine Bug variant</smallcaps>";
			}
		}

		// Token: 0x020021D0 RID: 8656
		public class VITAL
		{
			// Token: 0x04009B08 RID: 39688
			public static LocString TITLE = "Coral Bug";

			// Token: 0x04009B09 RID: 39689
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BD1 RID: 11217
			public class BODY
			{
				// Token: 0x0400BF23 RID: 48931
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Coral\" Shine Bug variant</smallcaps>";

				// Token: 0x0400BF24 RID: 48932
				public static LocString CONTAINER2 = "It is unwise to touch a Shine Bug's wing blades directly due to the extremely fragile nature of their membranes.";
			}
		}

		// Token: 0x020021D1 RID: 8657
		public class ROYAL
		{
			// Token: 0x04009B0A RID: 39690
			public static LocString TITLE = "Royal Bug";

			// Token: 0x04009B0B RID: 39691
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BD2 RID: 11218
			public class BODY
			{
				// Token: 0x0400BF25 RID: 48933
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Royal\" Shine Bug variant</smallcaps>";

				// Token: 0x0400BF26 RID: 48934
				public static LocString CONTAINER2 = "The Shine Bug can be pet anywhere else along its body, although it is advised that care still be taken due to the generally delicate nature of its exoskeleton.";
			}
		}

		// Token: 0x020021D2 RID: 8658
		public class SLICKSTER
		{
			// Token: 0x04009B0C RID: 39692
			public static LocString SPECIES_TITLE = "Slicksters";

			// Token: 0x04009B0D RID: 39693
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009B0E RID: 39694
			public static LocString TITLE = "Slickster";

			// Token: 0x04009B0F RID: 39695
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BD3 RID: 11219
			public class BODY
			{
				// Token: 0x0400BF27 RID: 48935
				public static LocString CONTAINER1 = "Slicksters are a unique creature most renowned for their ability to exude hydrocarbon waste that is nearly identical in makeup to crude oil.\n\nThe two tufts atop a Slickster's head are called rhinophores, and help guide the Slickster toward breathable carbon dioxide.";
			}
		}

		// Token: 0x020021D3 RID: 8659
		public class MOLTEN
		{
			// Token: 0x04009B10 RID: 39696
			public static LocString TITLE = "Molten Slickster";

			// Token: 0x04009B11 RID: 39697
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BD4 RID: 11220
			public class BODY
			{
				// Token: 0x0400BF28 RID: 48936
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Molten\" Slickster variant</smallcaps>";

				// Token: 0x0400BF29 RID: 48937
				public static LocString CONTAINER2 = "Slicksters are amicable creatures famous amongst breeders for their good personalities and smiley faces.\n\nSlicksters rarely if ever nip at human handlers, and are considered non-ideal house pets only for the oily mess they involuntarily leave behind wherever they go.";
			}
		}

		// Token: 0x020021D4 RID: 8660
		public class DECOR
		{
			// Token: 0x04009B12 RID: 39698
			public static LocString TITLE = "Longhair Slickster";

			// Token: 0x04009B13 RID: 39699
			public static LocString SUBTITLE = "Critter Morph";

			// Token: 0x02002BD5 RID: 11221
			public class BODY
			{
				// Token: 0x0400BF2A RID: 48938
				public static LocString CONTAINER1 = "<smallcaps>Pictured: \"Longhair\" Slickster variant</smallcaps>";

				// Token: 0x0400BF2B RID: 48939
				public static LocString CONTAINER2 = "Positioned on either side of the Major Rhinophores are Minor Rhinophores, which specialize in mechanical reception and detect air pressure around the Slickster. These send signals to the brain to contract or expand its air sacks accordingly.";
			}
		}

		// Token: 0x020021D5 RID: 8661
		public class STEGO
		{
			// Token: 0x04009B14 RID: 39700
			public static LocString SPECIES_TITLE = "Lumbs";

			// Token: 0x04009B15 RID: 39701
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009B16 RID: 39702
			public static LocString TITLE = "Lumb";

			// Token: 0x04009B17 RID: 39703
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BD6 RID: 11222
			public class BODY
			{
				// Token: 0x0400BF2C RID: 48940
				public static LocString CONTAINER1 = "While other creatures with the Lumb's low forebrain neuron count rapidly become extinct, these docile creatures are uniquely positioned for survival.\n\nTheir large size and spiny protrusions make them unappealing to most predators. Anecdotal evidence also suggests that smaller species will sometimes adopt orphaned Lumbs.\n\nSome experts theorize that this may be motivated by a desire to benefit from the Lumb's genetic muscular condition (akin to restless leg syndrome), which causes them to regularly pound the earth in such a way as to cause nearby plants to drop desirable foods.";
			}
		}

		// Token: 0x020021D6 RID: 8662
		public class SWEEPY
		{
			// Token: 0x04009B18 RID: 39704
			public static LocString TITLE = "Sweepy";

			// Token: 0x04009B19 RID: 39705
			public static LocString SUBTITLE = "Cleaning Robot";

			// Token: 0x02002BD7 RID: 11223
			public class BODY
			{
				// Token: 0x0400BF2D RID: 48941
				public static LocString CONTAINER1 = "The Sweepy is a domesticated sweeping robot programmed to clean solid and liquid debris. The " + UI.FormatAsLink("Sweepy's Dock", "SWEEPBOTSTATION") + " will automatically launch the Sweepy, store the debris the robot picks up, and recharge the Sweepy's battery, provided it has been plugged into a power source.\n\nThough the Sweepy can not travel over gaps or uneven ground, it is programmed to feel really bad about this.";
			}
		}

		// Token: 0x020021D7 RID: 8663
		public class DEERSPECIES
		{
			// Token: 0x04009B1A RID: 39706
			public static LocString SPECIES_TITLE = "Floxes";

			// Token: 0x04009B1B RID: 39707
			public static LocString SPECIES_SUBTITLE = "Critter Species";

			// Token: 0x04009B1C RID: 39708
			public static LocString TITLE = "Flox";

			// Token: 0x04009B1D RID: 39709
			public static LocString SUBTITLE = "Domesticable Critter";

			// Token: 0x02002BD8 RID: 11224
			public class BODY
			{
				// Token: 0x0400BF2E RID: 48942
				public static LocString CONTAINER1 = "Evenly distributed throughout the Flox's dense overcoat are countless vibrissae-like hairs that transmit detailed sensory information about its environment, allowing it to detect changes as subtle as the shift in another creature's mood.\n\nFloxes avoid overstimulation by whipping their tails to release the pent-up energy. Because these tactile hairs are so sensitive, they cannot be safely shorn.\n\nFlox antlers, however, are nerveless and cumbersome. Handlers who unburden them of this cranial load are often rewarded with the critter's long, slow blinks of contentment.";
			}
		}

		// Token: 0x020021D8 RID: 8664
		public class DUPLICANT
		{
			// Token: 0x04009B1E RID: 39710
			public static LocString SPECIES_TITLE = "Duplicants";

			// Token: 0x04009B1F RID: 39711
			public static LocString SPECIES_SUBTITLE = "Colony Workers";

			// Token: 0x02002BD9 RID: 11225
			public class BODY
			{
				// Token: 0x0400BF2F RID: 48943
				public static LocString CONTAINER1 = "Duplicants are printed at the " + UI.FormatAsLink("Printing Pod", "HEADQUARTERS") + ", emerging fully formed and clothed in standard-issue uniforms. Unique outfits can be found in the Supply Closet.\n\n";
			}
		}

		// Token: 0x020021D9 RID: 8665
		public class STANDARD
		{
			// Token: 0x04009B20 RID: 39712
			public static LocString TITLE = "Standard Duplicant";

			// Token: 0x04009B21 RID: 39713
			public static LocString SUBTITLE = "Colony Worker";

			// Token: 0x04009B22 RID: 39714
			public static LocString HEADER_1 = "Basic Needs";

			// Token: 0x04009B23 RID: 39715
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				UI.FormatAsLink("Toilets", "REQUIREMENTCLASSTOILETTYPE"),
				": Duplicants will empty their bladders every {time}. A lack of accessible facilities will result in ",
				UI.FormatAsLink("Stress", "STRESS"),
				" and wet colony floors.\n\n",
				UI.FormatAsLink("Oxygen", "BUILDCATEGORYOXYGEN"),
				": Duplicants inhale ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" at a rate of {O2gperSec} and exhale ",
				UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
				" at a rate of {CO2gperSec}. They can hold their breath for short periods. Long-term lack of breathable gases will result in suffocation.\n\n",
				UI.FormatAsLink("Food", "FOOD"),
				": Daily caloric intake is {caloriesrequired}. ",
				UI.FormatAsLink("Food", "BUILDCATEGORYFOOD"),
				" can be produced via ",
				UI.FormatAsLink("Farming", "GROUPFARMBUILDING"),
				" and ",
				UI.FormatAsLink("Ranching", "REQUIREMENTCLASSRANCHSTATIONTYPE"),
				" buildings, and further enhanced at ",
				UI.FormatAsLink("Cooking Stations", "REQUIREMENTCLASSCOOKTOP"),
				".\n\n",
				UI.FormatAsLink("Sleep", "HEALTH"),
				": Duplicants require a ",
				UI.FormatAsLink("Bed", "REQUIREMENTCLASSBEDTYPE"),
				" and a ",
				UI.FormatAsLink("Schedule", "MISCELLANEOUSTIPS14"),
				" that includes adequate Bedtime in order to avoid the ",
				UI.FormatAsLink("Stress", "STRESS"),
				" and Stamina-depleting effects of overwork.\n\n"
			});

			// Token: 0x04009B24 RID: 39716
			public static LocString HEADER_2 = "Worker Optimization";

			// Token: 0x04009B25 RID: 39717
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				UI.FormatAsLink("Skills", "ROLES"),
				": Performing colony duties helps Duplicants earn Skill Points that can be exchanged for useful Skills. Duplicants' individual traits may predispose them to prefer some careers over others, or bar them from a particular career path entirely.\n\n",
				UI.FormatAsLink("Morale", "MORALE"),
				": Morale in excess of a Duplicant's expectations will trigger Overjoyed responses that positively affect a variety of colony functions. ",
				UI.FormatAsLink("Recreational", "REQUIREMENTCLASSRECBUILDING"),
				" building usage, ",
				UI.FormatAsLink("attractive buildings ", "REQUIREMENTCLASSDECORATION"),
				" that increase ",
				UI.FormatAsLink("Decor", "DECOR"),
				", and improved ",
				UI.FormatAsLink("Foods", "FOOD"),
				" contribute to higher morale.\n\n",
				UI.FormatAsLink("Stress", "STRESS"),
				": When Stress levels reach 100%, Duplicants will exhibit negative Stress responses that can disrupt work and damage buildings.\n\n",
				UI.FormatAsLink("Research", "TECH"),
				": Using ",
				UI.FormatAsLink("science buildings", "REQUIREMENTCLASSSCIENCEBUILDING"),
				" unlocks advanced technologies that increase work efficiency and improve the colony's standard of living.\n\n",
				UI.FormatAsLink("Health", "HEALTH"),
				": Workplace hazards, including exposure to extreme ",
				UI.FormatAsLink("Heat", "HEAT"),
				" or ",
				UI.FormatAsLink("Germs", "DISEASE"),
				", can severely impact Duplicants' health. Specialized ",
				UI.FormatAsLink("Medical", "REQUIREMENTCLASSCLINIC"),
				" buildings accelerate recovery.\n\n<i>More information about sustaining Duplicants' well-being is covered in ",
				UI.FormatAsLink("Tips", "MISCELLANEOUSTIPS"),
				" and ",
				UI.FormatAsLink("Tutorials", "LESSONS"),
				".</i>\n\n"
			});
		}

		// Token: 0x020021DA RID: 8666
		public class BIONIC
		{
			// Token: 0x04009B26 RID: 39718
			public static LocString TITLE = "Bionic Duplicant";

			// Token: 0x04009B27 RID: 39719
			public static LocString SUBTITLE = "Specialized Colony Worker";

			// Token: 0x04009B28 RID: 39720
			public static LocString HEADER_1 = "Basic Needs";

			// Token: 0x04009B29 RID: 39721
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				UI.FormatAsLink("Power", "POWER"),
				": Bionic Duplicants run on portable ",
				UI.FormatAsLink("Power Banks", "ELECTROBANK"),
				" that they automatically replace during scheduled Downtime. If power banks are depleted, the Bionic Duplicant will become Powerless and may perish if they deplete their ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" tanks before being rebooted.\n\n",
				UI.FormatAsLink("Gunk Extractors", "GUNKEMPTIER"),
				": Bionic systems must dispose of built-up ",
				UI.FormatAsLink("Gunk", "LIQUIDGUNK"),
				" every {time}, or risk making a mess. If there are no purpose-built extractors available, Bionic Duplicants will clog a nearby ",
				UI.FormatAsLink("Toilet", "REQUIREMENTCLASSTOILETTYPE"),
				".\n\n",
				UI.FormatAsLink("Oxygen", "BUILDCATEGORYOXYGEN"),
				": Bionic Duplicants ventilate their mechanisms using internal ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" tanks. These must be refilled every {number} to prevent suffocation. They do not produce ",
				UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
				".\n\n",
				UI.FormatAsLink("Gear Oil", "LUBRICATINGOIL"),
				": Maintaining efficient operation of bionic systems requires visits to the ",
				UI.FormatAsLink("Lubrication Station", "OILCHANGER"),
				" or use of ",
				UI.FormatAsLink("Gear Balm", "LUBRICATIONSTICK"),
				". If neither are available, workers slow down to avoid grinding gears.\n\n"
			});

			// Token: 0x04009B2A RID: 39722
			public static LocString HEADER_2 = "Worker Optimization";

			// Token: 0x04009B2B RID: 39723
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				UI.FormatAsLink("Boosters", "BOOSTER"),
				": Bionic Duplicants gain specialized building usage and increase attributes by installing boosters. These can be added or removed at any time to customize a Bionic Duplicant's career path or mitigate the consequences of bionic bugs.\n\n",
				UI.FormatAsLink("Skills", "ROLES"),
				": Performing colony duties helps Bionic Duplicants earn skill points that can be exchanged for skills that may increase attributes and expand their capacity to install ",
				UI.FormatAsLink("Boosters", "BOOSTER"),
				" and ",
				UI.FormatAsLink("Power Banks", "ELECTROBANK"),
				".\n\n",
				UI.FormatAsLink("Morale", "MORALE"),
				": Morale in excess of a Duplicant's expectations will trigger Overjoyed responses that positively affect a variety of colony functions. ",
				UI.FormatAsLink("Recreational", "REQUIREMENTCLASSRECBUILDING"),
				" building usage, ",
				UI.FormatAsLink("attractive buildings ", "REQUIREMENTCLASSDECORATION"),
				" that increase ",
				UI.FormatAsLink("Decor", "DECOR"),
				", and improved ",
				UI.FormatAsLink("Foods", "FOOD"),
				" contribute to higher morale.\n\n",
				UI.FormatAsLink("Stress", "STRESS"),
				": When Stress levels reach 100%, Bionic Duplicants will exhibit negative Stress responses that can disrupt work and damage buildings.\n\n",
				UI.FormatAsLink("Research", "TECH"),
				": Using ",
				UI.FormatAsLink("science buildings", "REQUIREMENTCLASSSCIENCEBUILDING"),
				" unlocks advanced technologies that increase work efficiency and improve the colony's standard of living.\n\n<i>More information about sustaining Bionic Duplicants' well-being is covered in ",
				UI.FormatAsLink("Tips", "MISCELLANEOUSTIPS"),
				" and ",
				UI.FormatAsLink("Tutorials", "LESSONS"),
				".</i>\n\n"
			});
		}

		// Token: 0x020021DB RID: 8667
		public class B6_AICONTROL
		{
			// Token: 0x04009B2C RID: 39724
			public static LocString TITLE = "Re: Objectionable Request";

			// Token: 0x04009B2D RID: 39725
			public static LocString TITLE2 = "SUBJECT: Objectionable Request";

			// Token: 0x04009B2E RID: 39726
			public static LocString TITLE3 = "SUBJECT: Re: Objectionable Request";

			// Token: 0x04009B2F RID: 39727
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002BDA RID: 11226
			public class BODY
			{
				// Token: 0x0400BF30 RID: 48944
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Director Stern</b><size=12><alpha=#AA> <jstern@gravitas.nova></size></color>\nFrom: <b>Dr. Broussard</b><size=12><alpha=#AA> <obroussard@gravitas.nova></color></size></smallcaps>\n------------------\n";

				// Token: 0x0400BF31 RID: 48945
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>Dr. Broussard</b><size=12><alpha=#AA> <obroussard@gravitas.nova></size></color>\nFrom: <b>Director Stern</b><alpha=#AA><size=12> <jstern@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF32 RID: 48946
				public static LocString CONTAINER2 = "<indent=5%>Director,\n\nEngineering has requested the brainmaps of all blueprint subjects for the development of a podlinked software and I am reluctant to oblige.\n\nI believe they are seeking a way to exert temporary control over implanted subjects, and I fear this avenue of research may be ethically unsound.</indent>";

				// Token: 0x0400BF33 RID: 48947
				public static LocString CONTAINER3 = "<indent=5%>Doctor,\n\nI understand your concerns, but engineering's newest project was conceived under my supervision.\n\nPlease give them any materials they require to move forward with their research.</indent>";

				// Token: 0x0400BF34 RID: 48948
				public static LocString CONTAINER4 = "<indent=5%>You can't be serious, Jacquelyn?</indent>";

				// Token: 0x0400BF35 RID: 48949
				public static LocString CONTAINER5 = "<indent=5%>You signed off on cranial chip implantation. Why would this be where you draw the line?\n\nIt would be an invaluable safety measure and protect your printing subjects.</indent>";

				// Token: 0x0400BF36 RID: 48950
				public static LocString CONTAINER6 = "<indent=5%>It just gives me a bad feeling.\n\nI can't stop you if you insist on going forward with this, but I'd ask that you remove me from the project.</indent>";

				// Token: 0x0400BF37 RID: 48951
				public static LocString SIGNATURE1 = "\n-Dr. Broussard\n<size=11>Bioengineering Department\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF38 RID: 48952
				public static LocString SIGNATURE2 = "\n-Director Stern\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021DC RID: 8668
		public class B51_ARTHISTORYREQUEST
		{
			// Token: 0x04009B30 RID: 39728
			public static LocString TITLE = "Re: Implant Database Request";

			// Token: 0x04009B31 RID: 39729
			public static LocString TITLE2 = "Implant Database Request";

			// Token: 0x04009B32 RID: 39730
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002BDB RID: 11227
			public class BODY
			{
				// Token: 0x0400BF39 RID: 48953
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Director Stern</b><size=12><alpha=#AA> <jstern@gravitas.nova></color></size>\nFrom: <b>Dr. Broussard</b><size=12><alpha=#AA> <obroussard@gravitas.nova></color></size></smallcaps>\n------------------\n";

				// Token: 0x0400BF3A RID: 48954
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>Dr. Broussard</b><alpha=#AA><size=12> <obroussard@gravitas.nova></size></color>\nFrom: <b>Director Stern</b><alpha=#AA><size=12> <jstern@gravitas.nova></color></smallcaps></size>\n------------------\n";

				// Token: 0x0400BF3B RID: 48955
				public static LocString CONTAINER2 = "<indent=5%>Director,\n\nI have been thinking, and it occurs to me that our subjects will likely travel outside our range of radio contact when establishing new colonies.\n\nColonies travel into the cosmos as representatives of humanity, and I believe it is our duty to preserve the planet's non-scientific knowledge in addition to practical information.\n\nI would like to make a formal request that comprehensive arts and cultural histories make their way onto the microchip databases.</indent>";

				// Token: 0x0400BF3C RID: 48956
				public static LocString CONTAINER3 = "<indent=5%>Doctor,\n\nIf there is room available after the necessary scientific and survival knowledge has been uploaded, I will see what I can do.</indent>";

				// Token: 0x0400BF3D RID: 48957
				public static LocString SIGNATURE1 = "\n-Dr. Broussard\n<size=11>Bioengineering Department\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF3E RID: 48958
				public static LocString SIGNATURE2 = "\n-Director Stern\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021DD RID: 8669
		public class A4_ATOMICONRECRUITMENT
		{
			// Token: 0x04009B33 RID: 39731
			public static LocString TITLE = "Results from Atomicon";

			// Token: 0x04009B34 RID: 39732
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BDC RID: 11228
			public class BODY
			{
				// Token: 0x0400BF3F RID: 48959
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Director Stern</b><alpha=#AA><size=12> <jstern@gravitas.nova></size></color>\nFrom: <b>Dr. Jones</b><alpha=#AA><size=12> <ejones@gravitas.nova></smallcaps>\n------------------\n";

				// Token: 0x0400BF40 RID: 48960
				public static LocString CONTAINER2 = "<indent=5%>Director,\n\nEverything went well. Broussard was reluctant at first, but she has little alternative given the nature of her work and the recent turn of events.\n\nShe can begin at your convenience.</indent>";

				// Token: 0x0400BF41 RID: 48961
				public static LocString SIGNATURE1 = "\nXOXO,\nDr. Jones\n\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021DE RID: 8670
		public class A3_DEVONSBLOG
		{
			// Token: 0x04009B35 RID: 39733
			public static LocString TITLE = "Re: devon's bloggg";

			// Token: 0x04009B36 RID: 39734
			public static LocString TITLE2 = "SUBJECT: devon's bloggg";

			// Token: 0x04009B37 RID: 39735
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BDD RID: 11229
			public class BODY
			{
				// Token: 0x0400BF42 RID: 48962
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Summers</b><alpha=#AA><size=12> <jsummers@gravitas.nova></size></color>\nFrom: <b>Dr. Jones</b><alpha=#AA><size=12> <ejones@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF43 RID: 48963
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>Dr. Jones</b><alpha=#AA><size=12> <ejones@gravitas.nova></size></color>\nFrom: <b>Dr. Summers</b><alpha=#AA><size=12> <jsummers@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF44 RID: 48964
				public static LocString CONTAINER1 = "<indent=5%>Oh my goddd I found out today that Devon's one of those people who takes pictures of their food and uploads them to some boring blog somewhere.\n\nYou HAVE to come to lunch with us and see, they spend so long taking pictures that the food gets cold and they have to ask the waiter to reheat it. It's SO FUNNY.</indent>";

				// Token: 0x0400BF45 RID: 48965
				public static LocString CONTAINER2 = "<indent=5%>Oh cool, Devon's writing a new post for <i>Toast of the Town</i>? I'd love to tag along and \"see how the sausage is made\" so to speak, haha.\n\nI'll see you guys in a bit! :)</indent>";

				// Token: 0x0400BF46 RID: 48966
				public static LocString CONTAINER3 = "<indent=5%>WAIT, Joshua, you read Devon's blog??</indent>";

				// Token: 0x0400BF47 RID: 48967
				public static LocString SIGNATURE1 = "\nXOXO,\nDr. Jones\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF48 RID: 48968
				public static LocString SIGNATURE2 = "\n-Dr. Summers\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021DF RID: 8671
		public class C5_ENGINEERINGCANDIDATE
		{
			// Token: 0x04009B38 RID: 39736
			public static LocString TITLE = "RE: Engineer Candidate?";

			// Token: 0x04009B39 RID: 39737
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002BDE RID: 11230
			public class BODY
			{
				// Token: 0x0400BF49 RID: 48969
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Director Stern</b><size=12><alpha=#AA> <jstern@gravitas.nova></size></color></smallcaps>\nFrom: <b>[REDACTED]</b>\n------------------\n";

				// Token: 0x0400BF4A RID: 48970
				public static LocString CONTAINER3 = "<indent=5%>Director, I think I've found the perfect engineer candidate to design our small-scale colony machines.\n-----------------------------------------------------------------------------------------------------\n</indent>";

				// Token: 0x0400BF4B RID: 48971
				public static LocString CONTAINER4 = "<indent=10%><smallcaps><b>Bringing Creative Workspace Ideas into the Industrial Setting</b>\n\nMichael E.E. Perlmutter is a rising star in the world industrial design, making a name for himself by cooking up playful workspaces for a work force typically left out of the creative conversation.\n\n\"Ergodynamic chairs have been done to death,\" says Perlmutter. \"What I'm interested in is redesigning the industrial space. There's no reason why a machine can't convey a sense of whimsy.\"\n\nIt's this philosophy that has launched Perlmutter to the top of a very short list of hot new industrial designers.</indent></smallcaps>";

				// Token: 0x0400BF4C RID: 48972
				public static LocString SIGNATURE1 = "\n[REDACTED]\n<size=11>Human Resources Coordinator\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E0 RID: 8672
		public class B7_FRIENDLYEMAIL
		{
			// Token: 0x04009B3A RID: 39738
			public static LocString TITLE = "Hiiiii!";

			// Token: 0x04009B3B RID: 39739
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002BDF RID: 11231
			public class BODY
			{
				// Token: 0x0400BF4D RID: 48973
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Techna</b><alpha=#AA><size=12> <ntechna@gravitas.nova></size></color>\nFrom: <b>Dr. Jones</b><alpha=#AA><size=12> <ejones@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF4E RID: 48974
				public static LocString CONTAINER1 = "<indent=5%>Omg, <i>hi</i> Nikola!\n\nHave you heard about the super weird thing that's been happening in the kitchen lately? Joshua's lunch has disappeared from the fridge like, every day for the past week!\n\nThere's a <i>ton</i> of cameras in that room too but all anyone can see is like this spiky blond hair behind the fridge door.\n\nSo <i>weird</i> right? ;)\n\nAnyway, totally unrelated, but our computer system's been having this <i>glitch</i> where datasets going back for like half a year get <i>totally</i> wiped for all employees with the initials \"N.T.\"\n\nIsn't it weird how specific that is? Don't worry though! I'm sure I'll have it fixed before it affects any of <i>your</i> work.\n\nByeee!</indent>";

				// Token: 0x0400BF4F RID: 48975
				public static LocString SIGNATURE1 = "\nXOXO,\nDr. Jones\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E1 RID: 8673
		public class B1_HOLLANDSDOG
		{
			// Token: 0x04009B3C RID: 39740
			public static LocString TITLE = "Re: dr. holland's dog";

			// Token: 0x04009B3D RID: 39741
			public static LocString TITLE2 = "dr. holland's dog";

			// Token: 0x04009B3E RID: 39742
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BE0 RID: 11232
			public class BODY
			{
				// Token: 0x0400BF50 RID: 48976
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Summers</b><size=10><alpha=#AA> <jsummers@gravitas.nova></size></color>\nFrom: <b>Dr. Jones</b><alpha=#AA><size=10> <ejones@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF51 RID: 48977
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>Dr. Jones</b><alpha=#AA><size=10> <ejones@gravitas.nova></size></color>\nFrom: <b>Dr. Summers</b><size=10><alpha=#AA> <jsummers@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF52 RID: 48978
				public static LocString CONTAINER1 = "<indent=5%>OMIGOD, every time I go into the break room now I get ambushed by Dr. Holland and he traps me in a 20 minute conversation about his new dog.\n\nLike, I GET it! Your puppy is cute! Why do you have like 400 different pictures of it on your phone, FROM THE SAME ANGLE?!\n\nSO annoying.</indent>";

				// Token: 0x0400BF53 RID: 48979
				public static LocString CONTAINER2 = "<indent=5%>Haha, I think it's nice, he really loves his dog. Oh! Did I show you the thing my cat did last night? She always falls asleep on my bed but this time she sprawled out on her back and her little tongue was poking out! So cute.\n\n<color=#F44A47>[BROKEN IMAGE]</color>\n<alpha=#AA>[121 MISSING ATTACHMENTS]</color></indent>";

				// Token: 0x0400BF54 RID: 48980
				public static LocString CONTAINER3 = "<indent=5%><i><b>UGHHHHHHHH!</b></i>\nYou're the worst!</indent>";

				// Token: 0x0400BF55 RID: 48981
				public static LocString SIGNATURE1 = "\nXOXO,\nDr. Jones\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF56 RID: 48982
				public static LocString SIGNATURE2 = "\n-Dr. Summers\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E2 RID: 8674
		public class JOURNALISTREQUEST
		{
			// Token: 0x04009B3F RID: 39743
			public static LocString TITLE = "Re: Call me";

			// Token: 0x04009B40 RID: 39744
			public static LocString TITLE2 = "Call me";

			// Token: 0x04009B41 RID: 39745
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002BE1 RID: 11233
			public class BODY
			{
				// Token: 0x0400BF57 RID: 48983
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Olowe</b><size=10><alpha=#AA> <aolowe@gravitas.nova></size></color>\nFrom: <b>Quinn Kelly</b><alpha=#AA><size=10> <editor@stemscoop.news></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF58 RID: 48984
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>[BCC: all]</b><alpha=#AA><size=10> </size></color>\nFrom: <b>Quinn Kelly</b><alpha=#AA><size=10> <editor@stemscoop.news></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF59 RID: 48985
				public static LocString CONTAINER1 = "<indent=5%>Dear colleagues, friends and community members,\n\nAfter nine deeply fulfilling years as editor of The STEM Scoop, I am stepping down to spend more time with my family.\n\nPlease give a warm welcome to Dorian Hearst, who will be taking over editorial management duties effective immediately.</indent>";

				// Token: 0x0400BF5A RID: 48986
				public static LocString CONTAINER2 = "<indent=5%>I don't know how you pulled it off, but Stern's office just called the paper and granted me an exclusive...and a tour of the Gravitas Facility. I owe you a beer. No - a case of beer. Six cases of beer!\n\nSeriously, thank you. I know you're in a difficult position but you've done the right thing. See you on Tuesday.</indent>";

				// Token: 0x0400BF5B RID: 48987
				public static LocString CONTAINER3 = "<indent=5%>I waited at the fountain for four hours. Where were you? This story is going to be huge. Call me.</indent>";

				// Token: 0x0400BF5C RID: 48988
				public static LocString CONTAINER4 = "<indent=5%>Dr. Olowe,\n\nI'm sorry - I know ambushing you at your home last night was a bad idea. But something is happening at Gravitas, and people need to know. Please call me.</indent>";

				// Token: 0x0400BF5D RID: 48989
				public static LocString SIGNATURE1 = "\n-Q\n------------------\n";

				// Token: 0x0400BF5E RID: 48990
				public static LocString SIGNATURE2 = "\nAll the best,\nQuinn Kelly\n------------------\n";
			}
		}

		// Token: 0x020021E3 RID: 8675
		public class B50_MEMORYCHIP
		{
			// Token: 0x04009B42 RID: 39746
			public static LocString TITLE = "Duplicant Memory Solution";

			// Token: 0x04009B43 RID: 39747
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002BE2 RID: 11234
			public class BODY
			{
				// Token: 0x0400BF5F RID: 48991
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Director Stern</b><alpha=#AA><size=12> <jstern@gravitas.nova></size></color>\nFrom: <b>[REDACTED]</b></smallcaps>\n------------------\n";

				// Token: 0x0400BF60 RID: 48992
				public static LocString CONTAINER2 = "<indent=5%>Director,\n\nI had a thought about how to solve your Duplicant memory problem.\n\nRather than attempt to access the subject's old memories, what if we were to embed all necessary information for colony survival into the printing process itself?\n\nThe amount of data engineering can store has grown exponentially over the last year. We should take advantage of the technological development.</indent>";

				// Token: 0x0400BF61 RID: 48993
				public static LocString SIGNATURE1 = "\n[REDACTED]\n<size=11>Engineering Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E4 RID: 8676
		public class MISSINGNOTES
		{
			// Token: 0x04009B44 RID: 39748
			public static LocString TITLE = "Re: Missing notes";

			// Token: 0x04009B45 RID: 39749
			public static LocString TITLE2 = "SUBJECT: Missing notes";

			// Token: 0x04009B46 RID: 39750
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002BE3 RID: 11235
			public class BODY
			{
				// Token: 0x0400BF62 RID: 48994
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Jones</b><alpha=#AA><size=12> <ejones@gravitas.nova></size></color>\nFrom: <b>Dr. Olowe</b><alpha=#AA><size=12> <aolowe@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF63 RID: 48995
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>Dr. Olowe</b><alpha=#AA><size=12> <aolowe@gravitas.nova></size></color>\nFrom: <b>Dr. Jones</b><alpha=#AA><size=12> <ejones@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF64 RID: 48996
				public static LocString EMAILHEADER3 = "<smallcaps>To: <b>Dr. Olowe</b><alpha=#AA><size=12> <aolowe@gravitas.nova></size></color>\nFrom: <b>Director Stern</b><alpha=#AA><size=12> <jstern@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF65 RID: 48997
				public static LocString CONTAINER1 = "<indent=5%>Hello Dr. Jones,\n\nHope you are well. Sorry to bother you- I believe that someone may have inappropriately accessed my computer.\n\nWhen I was logging in this morning, the \"last log-in\" pop-up indicated that my computer had been accessed at 2 a.m. My last actual log-in was 6 p.m. And some of my files have gone missing.\n\nThe privacy of my work is paramount. Would it be possible to have someone take a look, please?</indent>";

				// Token: 0x0400BF66 RID: 48998
				public static LocString CONTAINER2 = "<indent=5%>OMG Amari, you're so dramatic!! It's probably just a glitch from the system network upgrade. Nobody can even get into your office without going through the new hand scanners.\n\nPS: Everybody's work is super private, not just yours ;)</indent>";

				// Token: 0x0400BF67 RID: 48999
				public static LocString CONTAINER3 = "<indent=5%>Dear Dr. Jones,\nI'm so sorry to bother you again...it's just that I'm absolutely certain that someone has been interfering with my files.\n\nI've noticed several discrepancies since last week's \"glitch.\" For example, responses to my recent employee survey on workplace satisfaction and safety were decrypted, and significant portions of my data and research notes have been erased. I'm even missing a few e-mails.\n\nIt's all quite alarming. Could you or Dr. Summers please investigate this when you have a moment?\n\nThank you so much,\n\n</indent>";

				// Token: 0x0400BF68 RID: 49000
				public static LocString CONTAINER4 = "<indent=5%>The files in question were a security risk, and were disposed of accordingly.\n\nAs for your emails: the NDA you signed was very clear about how to handle requests from members of the media.\n\nSee me in my office.</indent>";

				// Token: 0x0400BF69 RID: 49001
				public static LocString SIGNATURE1 = "\n-Dr. Olowe\n<size=11>Industrial-Organizational Psychologist\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF6A RID: 49002
				public static LocString SIGNATURE2 = "\nXOXO,\nDr. Jones\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF6B RID: 49003
				public static LocString SIGNATURE3 = "\n-Director Stern\n<size=11>\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E5 RID: 8677
		public class B4_MYPENS
		{
			// Token: 0x04009B47 RID: 39751
			public static LocString TITLE = "SUBJECT: MY PENS";

			// Token: 0x04009B48 RID: 39752
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BE4 RID: 11236
			public class BODY
			{
				// Token: 0x0400BF6C RID: 49004
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>ALL</b>\nFrom: <b>[REDACTED]</b></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF6D RID: 49005
				public static LocString CONTAINER2 = "<indent=5%>To whomever is stealing the glitter pens off of my desk:\n\n<i>CONSIDER THIS YOUR LAST WARNING!</i></indent>";

				// Token: 0x0400BF6E RID: 49006
				public static LocString SIGNATURE1 = "\nXOXO,\n[REDACTED]\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E6 RID: 8678
		public class A7_NEWEMPLOYEE
		{
			// Token: 0x04009B49 RID: 39753
			public static LocString TITLE = "Welcome, New Employee";

			// Token: 0x04009B4A RID: 39754
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002BE5 RID: 11237
			public class BODY
			{
				// Token: 0x0400BF6F RID: 49007
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>All</b>\nFrom: <b>[REDACTED]</b></smallcaps>\n------------------\n";

				// Token: 0x0400BF70 RID: 49008
				public static LocString CONTAINER2 = "<indent=5%>Attention Gravitas Facility personnel;\n\nPlease welcome our newest staff member, Olivia Broussard, PhD.\n\nDr. Broussard will be leading our upcoming genetics project and has been installed in our bioengineering department.\n\nBe sure to offer her our warmest welcome.</indent>";

				// Token: 0x0400BF71 RID: 49009
				public static LocString SIGNATURE1 = "\n[REDACTED]\n<size=11>Personnel Coordinator\nThe Gravitas Facility</indent>\n------------------\n";
			}
		}

		// Token: 0x020021E7 RID: 8679
		public class A6_NEWSECURITY2
		{
			// Token: 0x04009B4B RID: 39755
			public static LocString TITLE = "New Security System?";

			// Token: 0x04009B4C RID: 39756
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002BE6 RID: 11238
			public class BODY
			{
				// Token: 0x0400BF72 RID: 49010
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>[REDACTED]</b>\nFrom: <b>[REDACTED]</b></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF73 RID: 49011
				public static LocString CONTAINER2 = "<indent=5%>So, the facility is introducing this new security system that scans your hand to unlock the doors. My question is, what exactly are they scanning?\n\nThe folks in engineering say the door device doesn't look like a fingerprint scanner, but the duo working over in bioengineering won't comment at all.\n\nI can't say I like it.</indent>";

				// Token: 0x0400BF74 RID: 49012
				public static LocString SIGNATURE1 = "\n[REDACTED]\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E8 RID: 8680
		public class A8_NEWSECURITY3
		{
			// Token: 0x04009B4D RID: 39757
			public static LocString TITLE = "They Stole Our DNA";

			// Token: 0x04009B4E RID: 39758
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002BE7 RID: 11239
			public class BODY
			{
				// Token: 0x0400BF75 RID: 49013
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>[REDACTED]</b>\nFrom: <b>[REDACTED]</b></smallcaps>\n------------------\n";

				// Token: 0x0400BF76 RID: 49014
				public static LocString CONTAINER2 = "<indent=5%>I'm almost certain now that the Facility's stolen our genetic information.\n\nForty-odd employees would make for mighty convenient lab rats, and even if we discovered what Gravitas did, we wouldn't have a lot of legal options. We can't exactly go to the public given the nature of our work.\n\nI can't stop thinking about what sort of experiments they might be conducting on my DNA, but I have to keep my mouth shut.\n\nI can't risk losing my job.</indent>";

				// Token: 0x0400BF77 RID: 49015
				public static LocString SIGNATURE1 = "\n[REDACTED]\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021E9 RID: 8681
		public class B8_POLITEREQUEST
		{
			// Token: 0x04009B4F RID: 39759
			public static LocString TITLE = "Polite Request";

			// Token: 0x04009B50 RID: 39760
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002BE8 RID: 11240
			public class BODY
			{
				// Token: 0x0400BF78 RID: 49016
				public static LocString EMAILHEADER = "<smallcaps>To: <b>All</b>\nFrom: <b>Admin</b> <alpha=#AA><admin@gravitas.nova></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF79 RID: 49017
				public static LocString CONTAINER1 = "<indent=5%>To whoever is entering Director Stern's office to move objects on her desk one inch to the left, please desist as she finds it quite unnerving.</indent>";

				// Token: 0x0400BF7A RID: 49018
				public static LocString SIGNATURE = "\nThank-you,\n-Admin\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021EA RID: 8682
		public class A0_PRELIMINARYCALCULATIONS
		{
			// Token: 0x04009B51 RID: 39761
			public static LocString TITLE = "Preliminary Calculations";

			// Token: 0x04009B52 RID: 39762
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002BE9 RID: 11241
			public class BODY
			{
				// Token: 0x0400BF7B RID: 49019
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>[REDACTED]</b>\nFrom: <b>[REDACTED]</b></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF7C RID: 49020
				public static LocString CONTAINER2 = "<indent=5%>Director,\n\nEven with dramatic optimization, we can't fit the massive volume of resources needed for a colony seed aboard the craft. Not even when calculating for a very small interplanetary travel duration.\n\nSome serious changes are gonna have to be made for this to work.</indent>";

				// Token: 0x0400BF7D RID: 49021
				public static LocString SIGNATURE1 = "\nXOXO,\n[REDACTED]\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021EB RID: 8683
		public class B4_REMYPENS
		{
			// Token: 0x04009B53 RID: 39763
			public static LocString TITLE = "Re: MY PENS";

			// Token: 0x04009B54 RID: 39764
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BEA RID: 11242
			public class BODY
			{
				// Token: 0x0400BF7E RID: 49022
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>ALL</b>\nFrom: <b>Admin</b><size=12><alpha=#AA> <admin@gravitas.nova></color></size></smallcaps>\n------------------\n";

				// Token: 0x0400BF7F RID: 49023
				public static LocString CONTAINER2 = "<indent=5%>We would like to remind staff not to use the CC: All function for intra-office issues.\n\nIn the event of disputes or disruptive work behavior within the facility, please speak to HR directly.\n\nWe thank-you for your restraint.</indent>";

				// Token: 0x0400BF80 RID: 49024
				public static LocString SIGNATURE1 = "\n-Admin\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021EC RID: 8684
		public class B3_RETEMPORALBOWUPDATE
		{
			// Token: 0x04009B55 RID: 39765
			public static LocString TITLE = "RE: To Otto (Spec Changes)";

			// Token: 0x04009B56 RID: 39766
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BEB RID: 11243
			public class BODY
			{
				// Token: 0x0400BF81 RID: 49025
				public static LocString TITLEALT = "To Otto (Spec Changes)";

				// Token: 0x0400BF82 RID: 49026
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Sklodowska</b><size=10><alpha=#AA> <msklodowska@gravitas.nova></size></color>\nFrom: <b>Mr. Kraus</b><alpha=#AA><size=10> <okraus@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF83 RID: 49027
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>Mr. Kraus</b><alpha=#AA><size=10> <okraus@gravitas.nova></size></color>\nFrom: <b>Dr. Sklodowska</b><size=10><alpha=#AA> <msklodowska@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF84 RID: 49028
				public static LocString CONTAINER1 = "Thanks Doctor.\n\nPS, if you hit the \"Reply\" button instead of composing a new e-mail it makes it easier for people to tell what you're replying to. :)\n\nI appreciate it!\n\nMr. Kraus\n<size=11>Physics Department\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF85 RID: 49029
				public static LocString CONTAINER2 = "Try not to take it too personally, it's probably just stress.\n\nThe Facility started going through a major overhaul not long before you got here, so I imagine the Director is having quite a time getting it all sorted out.\n\nThings will calm down once all the new departments are settled.\n\nDr. Sklodowska\n<size=11>Physics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021ED RID: 8685
		public class A1_RESEARCHGIANTARTICLE
		{
			// Token: 0x04009B57 RID: 39767
			public static LocString TITLE = "Re: Have you seen this?";

			// Token: 0x04009B58 RID: 39768
			public static LocString TITLE2 = "SUBJECT: Have you seen this?";

			// Token: 0x04009B59 RID: 39769
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002BEC RID: 11244
			public class BODY
			{
				// Token: 0x0400BF86 RID: 49030
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Director Stern</b><size=12><alpha=#AA> <jstern@gravitas.nova></size></color>\nFrom: <b>[REDACTED]</b></smallcaps>\n------------------\n";

				// Token: 0x0400BF87 RID: 49031
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>[REDACTED]</b>\nFrom: <b>Director Stern</b><size=12><alpha=#AA> <jstern@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF88 RID: 49032
				public static LocString CONTAINER2 = "<indent=5%>Please pay it no mind. If any of these journals reach out to you, deny comment.</indent>";

				// Token: 0x0400BF89 RID: 49033
				public static LocString CONTAINER3 = "<indent=5%>Director, are you aware of the articles that have been cropping up about us lately?</indent>";

				// Token: 0x0400BF8A RID: 49034
				public static LocString CONTAINER4 = "<indent=10%><color=#F44A47>>[BROKEN LINK]</color> <alpha=#AA><smallcaps>the gravitas facility: questionable rise of a research giant</smallcaps></indent></color>";

				// Token: 0x0400BF8B RID: 49035
				public static LocString SIGNATURE1 = "\n[REDACTED]\n<size=11>Personnel Coordinator\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF8C RID: 49036
				public static LocString SIGNATURE2 = "\n-Director Stern\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021EE RID: 8686
		public class B2_TEMPORALBOWUPDATE
		{
			// Token: 0x04009B5A RID: 39770
			public static LocString TITLE = "Spec Changes";

			// Token: 0x04009B5B RID: 39771
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BED RID: 11245
			public class BODY
			{
				// Token: 0x0400BF8D RID: 49037
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Sklodowska</b><size=10><alpha=#AA> <msklodowska@gravitas.nova></size></color>\nFrom: <b>Mr. Kraus</b><alpha=#AA><size=10> <okraus@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF8E RID: 49038
				public static LocString CONTAINER2 = "Dr. Sklodowska, could I ask you to forward me the new spec changes to the Temporal Bow?\n\nThe Director completely ignored me when I asked for a project update this morning. She walked right past me in the hall - I didn't realize I was that far down on the food chain. :(\n\nMr. Kraus\nPhysics Department\nThe Gravitas Facility";
			}
		}

		// Token: 0x020021EF RID: 8687
		public class A5_THEJANITOR
		{
			// Token: 0x04009B5C RID: 39772
			public static LocString TITLE = "Re: omg the janitor";

			// Token: 0x04009B5D RID: 39773
			public static LocString TITLE2 = "SUBJECT: Re: omg the janitor";

			// Token: 0x04009B5E RID: 39774
			public static LocString TITLE3 = "SUBJECT: omg the janitor";

			// Token: 0x04009B5F RID: 39775
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BEE RID: 11246
			public class BODY
			{
				// Token: 0x0400BF8F RID: 49039
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Dr. Summers</b><size=12><alpha=#AA> <jsummers@gravitas.nova></color></size>\nFrom: <b>Dr. Jones</b><size=12><alpha=#AA> <ejones@gravitas.nova></color></size></smallcaps>\n------------------\n";

				// Token: 0x0400BF90 RID: 49040
				public static LocString EMAILHEADER2 = "<smallcaps>To: <b>Dr. Jones</b><size=12><alpha=#AA> <ejones@gravitas.nova></color></size>\nFrom: <b>Dr. Summers</b><size=12><alpha=#AA> <jsummers@gravitas.nova></color></size></smallcaps>\n------------------\n";

				// Token: 0x0400BF91 RID: 49041
				public static LocString CONTAINER2 = "<indent=5%><i>Pfft,</i> whatever.</indent>";

				// Token: 0x0400BF92 RID: 49042
				public static LocString CONTAINER3 = "<indent=5%>Aw, he's really nice if you get to know him though. Really dependable too. One time I busted a wheel off my office chair and he got me a new one in like, two minutes. I think he's just sweaty because he works so hard.</indent>";

				// Token: 0x0400BF93 RID: 49043
				public static LocString CONTAINER4 = "<indent=5%>OMIGOSH have you seen our building's janitor? He totally smells and he has sweat stains under his armpits like EVERY time I see him. SO embarrassing.</indent>";

				// Token: 0x0400BF94 RID: 49044
				public static LocString SIGNATURE1 = "\nXOXO,\nDr. Jones\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";

				// Token: 0x0400BF95 RID: 49045
				public static LocString SIGNATURE2 = "\n-Dr. Summers\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021F0 RID: 8688
		public class A2_THERMODYNAMICLAWS
		{
			// Token: 0x04009B60 RID: 39776
			public static LocString TITLE = "The Laws of Thermodynamics";

			// Token: 0x04009B61 RID: 39777
			public static LocString SUBTITLE = "UNENCRYPTED";

			// Token: 0x02002BEF RID: 11247
			public class BODY
			{
				// Token: 0x0400BF96 RID: 49046
				public static LocString EMAILHEADER1 = "<smallcaps>To: <b>Mr. Kraus</b><alpha=#AA><size=12> <okraus@gravitas.nova></size></color>\nFrom: <b>Dr. Jones</b><alpha=#AA><size=12> <ejones@gravitas.nova></size></color></smallcaps>\n------------------\n";

				// Token: 0x0400BF97 RID: 49047
				public static LocString CONTAINER1 = "<indent=5%><i>Hello</i> Mr. Kraus!\n\nI was just e-mailing you after our little chat today to pass along something you might like to read - I think you'll find it super useful in your research!\n\n</indent>";

				// Token: 0x0400BF98 RID: 49048
				public static LocString CONTAINER2 = "<indent=10%><b>FIRST LAW</b></indent>\n<indent=15%>Energy can neither be created or destroyed, only change forms.</indent>";

				// Token: 0x0400BF99 RID: 49049
				public static LocString CONTAINER3 = "<indent=10%><b>SECOND LAW</b></indent>\n<indent=15%>Entropy in an isolated system that is not in equilibrium tends to increase over time, approaching the maximum value at equilibrium.</indent>";

				// Token: 0x0400BF9A RID: 49050
				public static LocString CONTAINER4 = "<indent=10%><b>THIRD LAW</b></indent>\n<indent=15%>Entropy in a system approaches a constant minimum as temperature approaches absolute zero.</indent>";

				// Token: 0x0400BF9B RID: 49051
				public static LocString CONTAINER5 = "<indent=10%><b>ZEROTH LAW</b></indent>\n<indent=15%>If two thermodynamic systems are in thermal equilibrium with a third, then they are in thermal equilibrium with each other.</indent>";

				// Token: 0x0400BF9C RID: 49052
				public static LocString CONTAINER6 = "<indent=5%>\nIf this is too complicated for you, you can come by to chat. I'd be <i>thrilled</i> to answer your questions. ;)</indent>";

				// Token: 0x0400BF9D RID: 49053
				public static LocString SIGNATURE1 = "\nXOXO,\nDr. Jones\n<size=11>Information and Statistics Department\nThe Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021F1 RID: 8689
		public class TIMEOFFAPPROVED
		{
			// Token: 0x04009B62 RID: 39778
			public static LocString TITLE = "Vacation Request Approved";

			// Token: 0x04009B63 RID: 39779
			public static LocString TITLE2 = "SUBJECT: Vacation Request Approved";

			// Token: 0x04009B64 RID: 39780
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002BF0 RID: 11248
			public class BODY
			{
				// Token: 0x0400BF9E RID: 49054
				public static LocString EMAILHEADER = "<smallcaps>To: <b>Dr. Ross</b><size=12><alpha=#AA> <dross@gravitas.nova></size></color>\nFrom: <b>Admin</b><size=12><alpha=#AA> <admin@gravitas.nova></color></size></smallcaps>\n------------------\n";

				// Token: 0x0400BF9F RID: 49055
				public static LocString CONTAINER = "<indent=5%><b>Vacation Request Granted</b>\nGood luck, Devon!\n\n<alpha=#AA><smallcaps><indent=10%> Vacation Request [May 18th-20th]\nReason: Time off request for attendance of the Blogjam Awards (\"Toast of the Town\" nominated in the Freshest Food Blog category).</indent></smallcaps></color></indent>";

				// Token: 0x0400BFA0 RID: 49056
				public static LocString SIGNATURE = "\n-Admin\n<size=11>The Gravitas Facility</size>\n------------------\n";
			}
		}

		// Token: 0x020021F2 RID: 8690
		public class BASIC_FABRIC
		{
			// Token: 0x04009B65 RID: 39781
			public static LocString TITLE = "Reed Fiber";

			// Token: 0x04009B66 RID: 39782
			public static LocString SUBTITLE = "Textile Ingredient";

			// Token: 0x02002BF1 RID: 11249
			public class BODY
			{
				// Token: 0x0400BFA1 RID: 49057
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"A ball of raw cellulose harvested from a ",
					UI.FormatAsLink("Thimble Reed", "BASICFABRICPLANT"),
					".\n\nIt is used in the production of ",
					UI.FormatAsLink("Clothing", "EQUIPMENT"),
					" and textiles."
				});
			}
		}

		// Token: 0x020021F3 RID: 8691
		public class BIONICBOOSTER
		{
			// Token: 0x04009B67 RID: 39783
			public static LocString TITLE = "Boosters";

			// Token: 0x04009B68 RID: 39784
			public static LocString SUBTITLE = "Bionic Systems";

			// Token: 0x02002BF2 RID: 11250
			public class BODY
			{
				// Token: 0x0400BFA2 RID: 49058
				public static LocString CONTAINER1 = "Boosters are programming modules designed to improve and expand Bionic Duplicants' abilities.\n\nBoosters consume a significant amount of processing power during access and implementation. Bionic Duplicants can expand their capacity for booster installation by accumulating skill points.\n\nBoosters can be combined to grant a broader range of skills, as well as to counteract bionic bugs that may exist in their original programming.";
			}
		}

		// Token: 0x020021F4 RID: 8692
		public class CRAB_SHELL
		{
			// Token: 0x04009B69 RID: 39785
			public static LocString TITLE = "Pokeshell Molt";

			// Token: 0x04009B6A RID: 39786
			public static LocString SUBTITLE = "Critter Byproduct";

			// Token: 0x04009B6B RID: 39787
			public static LocString CONTAINER1 = "An exoskeleton discarded by an aquatic critter.\n\n";

			// Token: 0x02002BF3 RID: 11251
			public class BABY_CRAB_SHELL
			{
				// Token: 0x0400BFA3 RID: 49059
				public static LocString TITLE = "Small Pokeshell Molt";

				// Token: 0x0400BFA4 RID: 49060
				public static LocString SUBTITLE = "Critter Byproduct";

				// Token: 0x0400BFA5 RID: 49061
				public static LocString CONTAINER1 = "An adorable little exoskeleton discarded by a baby aquatic critter.";
			}
		}

		// Token: 0x020021F5 RID: 8693
		public class DATABANK
		{
			// Token: 0x04009B6C RID: 39788
			public static LocString TITLE = "Data Banks";

			// Token: 0x04009B6D RID: 39789
			public static LocString SUBTITLE = "Information Technology";

			// Token: 0x02002BF4 RID: 11252
			public class BODY
			{
				// Token: 0x0400BFA6 RID: 49062
				public static LocString CONTAINER1 = "Data Banks are a form of portable storage media. They are prized for their non-volatility, robustness, and practical research applications.\n\nThey are not foldable.";
			}
		}

		// Token: 0x020021F6 RID: 8694
		public class DEWDRIP
		{
			// Token: 0x04009B6E RID: 39790
			public static LocString TITLE = "Dewdrip";

			// Token: 0x04009B6F RID: 39791
			public static LocString SUBTITLE = "Plant Byproduct";

			// Token: 0x02002BF5 RID: 11253
			public class BODY
			{
				// Token: 0x0400BFA7 RID: 49063
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"A crystallized blob of ",
					UI.FormatAsLink("Brackene", "MILK"),
					" from the ",
					UI.FormatAsLink("Dew Dripper", "DEWDRIPPERPLANT"),
					".\n\nIt must be processed at the ",
					UI.FormatAsLink("Plant Pulverizer", "MILKPRESS"),
					" to release its contents."
				});
			}
		}

		// Token: 0x020021F7 RID: 8695
		public class EGG_SHELL
		{
			// Token: 0x04009B70 RID: 39792
			public static LocString TITLE = "Egg Shell";

			// Token: 0x04009B71 RID: 39793
			public static LocString SUBTITLE = "Critter Byproduct";

			// Token: 0x02002BF6 RID: 11254
			public class BODY
			{
				// Token: 0x0400BFA8 RID: 49064
				public static LocString CONTAINER1 = "The shards left over from the protective walls of a baby critter's first home.";
			}
		}

		// Token: 0x020021F8 RID: 8696
		public class ELECTROBANK
		{
			// Token: 0x04009B72 RID: 39794
			public static LocString TITLE = "Power Banks";

			// Token: 0x04009B73 RID: 39795
			public static LocString SUBTITLE = "Portable Storage";

			// Token: 0x02002BF7 RID: 11255
			public class BODY
			{
				// Token: 0x0400BFA9 RID: 49065
				public static LocString CONTAINER1 = "Power Banks are portable " + UI.FormatAsLink("Power", "POWER") + " storage containers that can be used to supply electricity to mobile entities and isolated areas.\n\nSingle-use power banks are easier to produce, but rechargeable and self-charging models are more efficient in the long run.\n\nCautious handling is required, as liquid exposure (or expiration, for self-charging power banks) can lead to explosions.";
			}
		}

		// Token: 0x020021F9 RID: 8697
		public class FEATHER_FABRIC
		{
			// Token: 0x04009B74 RID: 39796
			public static LocString TITLE = "Feather Fiber";

			// Token: 0x04009B75 RID: 39797
			public static LocString SUBTITLE = "Textile Ingredient";

			// Token: 0x02002BF8 RID: 11256
			public class BODY
			{
				// Token: 0x0400BFAA RID: 49066
				public static LocString CONTAINER1 = "A stalk of raw keratin used in the production of " + UI.FormatAsLink("Clothing", "EQUIPMENT") + " and textiles.";
			}
		}

		// Token: 0x020021FA RID: 8698
		public class VARIANT_GOLD
		{
			// Token: 0x04009B76 RID: 39798
			public static LocString TITLE = "Regal Bammoth Crest";

			// Token: 0x04009B77 RID: 39799
			public static LocString SUBTITLE = "Critter Byproduct";

			// Token: 0x02002BF9 RID: 11257
			public class BODY
			{
				// Token: 0x0400BFAB RID: 49067
				public static LocString CONTAINER1 = "Heavy was the head that wore this crest, until it was relieved of its burden by a helpful Duplicant.";
			}
		}

		// Token: 0x020021FB RID: 8699
		public class KELP
		{
			// Token: 0x04009B78 RID: 39800
			public static LocString TITLE = "Seakomb Leaf";

			// Token: 0x04009B79 RID: 39801
			public static LocString SUBTITLE = "Plant Byproduct";

			// Token: 0x02002BFA RID: 11258
			public class BODY
			{
				// Token: 0x0400BFAC RID: 49068
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"The leaf of a ",
					UI.FormatAsLink("Seakomb", "KELPPLANT"),
					".\n\nIt can be processed into ",
					UI.FormatAsLink("Phyto Oil", "PHYTOOIL"),
					" or used as an ingredient in ",
					UI.FormatAsLink("Allergy Medication", "ANTIHISTAMINE "),
					"."
				});
			}
		}

		// Token: 0x020021FC RID: 8700
		public class LUMBER
		{
			// Token: 0x04009B7A RID: 39802
			public static LocString TITLE = "Wood";

			// Token: 0x04009B7B RID: 39803
			public static LocString SUBTITLE = "Renewable Resource";

			// Token: 0x02002BFB RID: 11259
			public class BODY
			{
				// Token: 0x0400BFAD RID: 49069
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"Thick logs of ",
					UI.FormatAsLink("Wood", "WOOD"),
					" harvested from ",
					UI.FormatAsLink("Arbor Trees", "FOREST_TREE"),
					", ",
					UI.FormatAsLink("Oakshells", "CRABWOOD"),
					" and other natural sources.\n\nWood Logs are used in the production of ",
					UI.FormatAsLink("Heat", "HEAT"),
					" and ",
					UI.FormatAsLink("Power", "POWER"),
					". They are also a useful building material."
				});
			}
		}

		// Token: 0x020021FD RID: 8701
		public class POWER_STATION_TOOLS
		{
			// Token: 0x04009B7C RID: 39804
			public static LocString TITLE = "Microchips";

			// Token: 0x04009B7D RID: 39805
			public static LocString SUBTITLE = "Specialized Equipment";

			// Token: 0x02002BFC RID: 11260
			public class BODY
			{
				// Token: 0x0400BFAE RID: 49070
				public static LocString CONTAINER1 = "Microchips are engineered tools containing countless lines of proprietary code. New applications are still being discovered.";
			}
		}

		// Token: 0x020021FE RID: 8702
		public class FARM_STATION_TOOLS
		{
			// Token: 0x04009B7E RID: 39806
			public static LocString TITLE = "Micronutrient Fertilizer";

			// Token: 0x04009B7F RID: 39807
			public static LocString SUBTITLE = "Specialized Farming Equipment";

			// Token: 0x02002BFD RID: 11261
			public class BODY
			{
				// Token: 0x0400BFAF RID: 49071
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"Micronutrient fertilizer is a specialized ",
					UI.FormatAsLink("Fertilizer", "FERTILIZER"),
					" produced at the ",
					UI.FormatAsLink("Farm Station", "FARMSTATION"),
					".\n\nIt must be crafted by Duplicants with the ",
					DUPLICANTS.ROLES.FARMER.NAME,
					" Skill, and helps ",
					UI.FormatAsLink("Plants", "PLANTS"),
					" grow faster."
				});
			}
		}

		// Token: 0x020021FF RID: 8703
		public class SWAMPLILYFLOWER
		{
			// Token: 0x04009B80 RID: 39808
			public static LocString TITLE = "Balm Lily Flower";

			// Token: 0x04009B81 RID: 39809
			public static LocString SUBTITLE = "Medicinal Herb";

			// Token: 0x02002BFE RID: 11262
			public class BODY
			{
				// Token: 0x0400BFB0 RID: 49072
				public static LocString CONTAINER1 = "Balm Lily Flowers bloom on " + UI.FormatAsLink("Balm Lily", "SWAMPLILY") + " plants.\n\nThey have a wide range of medicinal applications, and have been shown to be a particularly effective antidote for respiratory illnesses.\n\nThe intense perfume emitted by their vivid petals is best described as \"dizzying.\"";
			}
		}

		// Token: 0x02002200 RID: 8704
		public class VARIANT_WOOD_SHELL
		{
			// Token: 0x04009B82 RID: 39810
			public static LocString TITLE = "Oakshell Molt";

			// Token: 0x04009B83 RID: 39811
			public static LocString SUBTITLE = "Critter Byproduct";

			// Token: 0x04009B84 RID: 39812
			public static LocString CONTAINER1 = "A splintery exoskeleton discarded by an aquatic critter.\n\n";

			// Token: 0x02002BFF RID: 11263
			public class BABY_VARIANT_WOOD_SHELL
			{
				// Token: 0x0400BFB1 RID: 49073
				public static LocString TITLE = "Small Oakshell Molt";

				// Token: 0x0400BFB2 RID: 49074
				public static LocString SUBTITLE = "Critter Byproduct";

				// Token: 0x0400BFB3 RID: 49075
				public static LocString CONTAINER1 = "A cute little splintery exoskeleton discarded by a baby aquatic critter.";
			}
		}

		// Token: 0x02002201 RID: 8705
		public class CRYOTANKWARNINGS
		{
			// Token: 0x04009B85 RID: 39813
			public static LocString TITLE = "CRYOTANK SAFETY";

			// Token: 0x04009B86 RID: 39814
			public static LocString SUBTITLE = "IMPORTANT OPERATING INSTRUCTIONS FOR THE CRYOTANK 3000";

			// Token: 0x02002C00 RID: 11264
			public class BODY
			{
				// Token: 0x0400BFB4 RID: 49076
				public static LocString CONTAINER1 = "    • Do not leave the contents of the Cryotank 3000 unattended unless an apocalyptic disaster has left you no choice.\n\n    • Ensure that the Cryotank 3000 has enough battery power to remain active for at least 6000 years.\n\n    • Do not attempt to defrost the contents of the Cryotank 3000 while it is submerged in molten hot lava.\n\n    • Use only a qualified Gravitas Cryotank repair facility to repair your Cryotank 3000. Attempting to service the device yourself will void the warranty.\n\n    • Do not put food in the Cryotank 3000. The Cryotank 3000 is not a refrigerator.\n\n    • Do not allow children to play in the Cryotank 3000. The Cryotank 3000 is not a toy.\n\n    • While the Cryotank 3000 is able to withstand a nuclear blast, Gravitas and its subsidiaries are not responsible for what may happen in the resulting nuclear fallout.\n\n    • Wait at least 5 minutes after being unfrozen from the Cryotank 3000 before operating heavy machinery.\n\n    • Each Cryotank 3000 is good for only one use.\n\n";
			}
		}

		// Token: 0x02002202 RID: 8706
		public class EVACUATION
		{
			// Token: 0x04009B87 RID: 39815
			public static LocString TITLE = "! EVACUATION NOTICE !";

			// Token: 0x04009B88 RID: 39816
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C01 RID: 11265
			public class BODY
			{
				// Token: 0x0400BFB5 RID: 49077
				public static LocString CONTAINER1 = "<smallcaps>Attention all Gravitas personnel\n\nEvacuation protocol in effect\n\nReactor meltdown in bioengineering imminent\n\nRemain calm and proceed to emergency exits\n\nDo not attempt to use elevators</smallcaps>";
			}
		}

		// Token: 0x02002203 RID: 8707
		public class C7_FIRSTCOLONY
		{
			// Token: 0x04009B89 RID: 39817
			public static LocString TITLE = "Director's Notes";

			// Token: 0x04009B8A RID: 39818
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C02 RID: 11266
			public class BODY
			{
				// Token: 0x0400BFB6 RID: 49078
				public static LocString CONTAINER1 = "The first experiments with establishing a colony off planet were an unmitigated disaster. Without outside help, our current Artificial Intelligence was completely incapable of making the kind of spontaneous decisions needed to deal with unforeseen circumstances. Additionally, the colony subjects lacked the forethought to even build themselves toilet facilities, even after soiling themselves repeatedly.\n\nWhile initial experiments in a lab setting were encouraging, our latest operation on non-Terra soil revealed some massive inadequacies to our system. If this idea is ever going to work, we will either need to drastically improve the AI directing the subjects, or improve the brains of our Duplicants to the point where they possess higher cognitive functions.\n\nGiven the disastrous complications that I could foresee arising if our Duplicants were made less supplicant, I'm leaning toward a push to improve our Artificial Intelligence.\n\nMeanwhile, we will have to send a clean-up crew to destroy all evidence of our little experiment beneath the Ceres' surface. We can't risk anyone discovering the remnants of our failed colony, even if that's unlikely to happen for another few decades at least.\n\n(Sometimes it boggles my mind how much further behind Gravitas the rest of the world is.)";
			}
		}

		// Token: 0x02002204 RID: 8708
		public class A8_FIRSTSUCCESS
		{
			// Token: 0x04009B8B RID: 39819
			public static LocString TITLE = "Encouraging Results";

			// Token: 0x04009B8C RID: 39820
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C03 RID: 11267
			public class BODY
			{
				// Token: 0x0400BFB7 RID: 49079
				public static LocString CONTAINER1 = "We've successfully compressed and expanded small portions of time under .03 milliseconds. This proves that time is something that can be physically acted upon, suggesting that our vision is attainable.\n\nAn unintentional consequence of both the expansion and contraction of time is the creation of a \"vacuum\" in the space between the affected portion of time and the much more expansive unaffected portions.\n\nSo far, we are seeing that the unaffected time on either side of the manipulated portion will expand or contract to fill the vacuum, although we are unsure how far-reaching this consequence is or what effect it has on the laws of the natural universe. At the end of all compression and expansion experiments, alterations to time are undone and leave no lasting change.";
			}
		}

		// Token: 0x02002205 RID: 8709
		public class B8_MAGAZINEARTICLE
		{
			// Token: 0x04009B8D RID: 39821
			public static LocString TITLE = "Nucleoid Article";

			// Token: 0x04009B8E RID: 39822
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C04 RID: 11268
			public class BODY
			{
				// Token: 0x0400BFB8 RID: 49080
				public static LocString CONTAINER1 = "<b>Incredible Technology From Independent Lab Harnesses Time into Energy</b>";

				// Token: 0x0400BFB9 RID: 49081
				public static LocString CONTAINER2 = "Scientists from the recently founded Gravitas Facility have unveiled their first technology prototype, dubbed the \"Temporal Bow\". It is a device which manipulates the 4th dimension to generate infinite, clean and renewable energy.\n\nWhile it may sound like something from science fiction, facility founder Dr. Jacquelyn Stern confirms that it is very much real.\n\n\"It has already been demonstrated that Newton's Second Law of Motion can be violated by negative mass superfluids under the correct lab conditions,\" she says.\n\n\"If the Laws of Motion can be bent and altered, why not the Laws of Thermodynamics? That was the main intent behind this project.\"\n\nThe Temporal Bow works by rapidly vibrating sections of the 4th dimension to send small quantities of mass forward and backward in time, generating massive amounts of energy with virtually no waste.\n\n\"The fantastic thing about using the 4th dimension as fuel,\" says Stern, \"is that it is really, categorically infinite\".\n\nFor those eagerly awaiting the prospect of human time travel, don't get your hopes up just yet. The Facility says that although they have successfully transported matter through time, the technology was expressly developed for the purpose of energy generation and is ill-equipped for human transportation.";
			}
		}

		// Token: 0x02002206 RID: 8710
		public class MYSTERYAWARD
		{
			// Token: 0x04009B8F RID: 39823
			public static LocString TITLE = "Nanotech Article";

			// Token: 0x04009B90 RID: 39824
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C05 RID: 11269
			public class BODY
			{
				// Token: 0x0400BFBA RID: 49082
				public static LocString CONTAINER1 = "<b>Mystery Project Wins Nanotech Award</b>";

				// Token: 0x0400BFBB RID: 49083
				public static LocString CONTAINER2 = "Last night's Worldwide Nanotech Awards has sparked controversy in the scientific community after it was announced that the top prize had been awarded to a project whose details could not be publicly disclosed.\n\nThe highly classified paper was presented to the jury in a closed session by lead researcher Dr. Liling Pei, recipient of the inaugural Gravitas Accelerator Scholarship at the Elion University of Science and Technology.\n\nHead judge Dr. Elias Balko acknowledges that it was unorthodox, but defends the decision. \"We're scientists - it's our job to push boundaries.\"\n\nPei was awarded the coveted Halas Medal, the top prize for innovation in the field.\n\n\"I wish I could tell you more,\" says Pei. \"I'm SO grateful to the WNA for this great honor, and to Dr. Stern for the funding that made it all possible. This is going to change everything about...well, everything.\"\n\nThis is the second time that Pei has made headlines. Last year, the striking young nanoscientist won the Miss Planetary Belle pageant's talent show with a live demonstration of nanorobots weaving a ballgown out of fibers harvested from common houseplants.\n\nPei joins the team at the Gravitas Facility early next month.";
			}
		}

		// Token: 0x02002207 RID: 8711
		public class A7_NEUTRONIUM
		{
			// Token: 0x04009B91 RID: 39825
			public static LocString TITLE = "Byproduct Notes";

			// Token: 0x04009B92 RID: 39826
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C06 RID: 11270
			public class BODY
			{
				// Token: 0x0400BFBC RID: 49084
				public static LocString CONTAINER1 = "[LOG BEGINS]\n\nDirector: I've determined the substance to be metallic in nature. The exact cause of its formation is still unknown, though I believe it to be something of an autoimmune reaction of the natural universe, a quarantining of foreign material to prevent temporal contamination.\n\nDirector: A method has yet to be found that can successfully remove the substance from an affected object, and the larger implication that two molecularly, temporally identical objects cannot coexist at one point in time has dire implications for all time manipulation technology research, not just the Bow.\n\nDirector: For the moment I have dubbed the substance \"Neutronium\", and assigned it a theoretical place on the table of elements. Research continues.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002208 RID: 8712
		public class A9_NEUTRONIUMAPPLICATIONS
		{
			// Token: 0x04009B93 RID: 39827
			public static LocString TITLE = "Possible Applications";

			// Token: 0x04009B94 RID: 39828
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C07 RID: 11271
			public class BODY
			{
				// Token: 0x0400BFBD RID: 49085
				public static LocString CONTAINER1 = "[LOG BEGINS]\n\nDirector: Temporal energy can be reconfigured to vibrate the matter constituting Neutronium at just the right frequency to break it down and disperse it.\n\nDirector: However, it is difficult to stabilize and maintain this reconfigured energy long enough to effectively remove practical amounts of Neutronium in real-life scenarios.\n\nDirector: I am looking into making this technology more reliable and compact - this data could potentially have uses in the development of some sort of all-purpose disintegration ray.\n\n[END LOG]";
			}
		}

		// Token: 0x02002209 RID: 8713
		public class PLANETARYECHOES
		{
			// Token: 0x04009B95 RID: 39829
			public static LocString TITLE = "Planetary Echoes";

			// Token: 0x04009B96 RID: 39830
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C08 RID: 11272
			public class BODY
			{
				// Token: 0x0400BFBE RID: 49086
				public static LocString TITLE1 = "Echo One";

				// Token: 0x0400BFBF RID: 49087
				public static LocString TITLE2 = "Echo Two";

				// Token: 0x0400BFC0 RID: 49088
				public static LocString CONTAINER1 = "Olivia: We've double-checked our observational equipment and the computer's warm-up is almost finished. We have precautionary personnel in place ready to start a shutdown in the event of a failure.\n\nOlivia: It's time.\n\nJackie: Right.\n\nJackie: Spin the machine up slowly so we can monitor for any abnormal power fluctuations. We start on \"3\".\n\nJackie: \"1\"... \"2\"...\n\nJackie: \"3\".\n\n[There's a metallic clunk. The baritone whirr of machinery can be heard.]\n\nJackie: Something's not right.\n\nOlivia: It's the container... the atom is vibrating too fast.\n\n[The whir of the machinery peels up an octave into a mechanical screech.]\n\nOlivia: W-we have to abort!\n\nJackie: No, not yet. Drop power from the coolant system and use it to bolster the container. It'll stabilize.\n\nOlivia: But without coolant--\n\nJackie: It will stabilize!\n\n[There's a sharp crackle of electricity.]\n\nOlivia: Drop 40% power from the coolant systems, reroute everything we have to the atomic container! \n\n[The whirring reaches a crescendo, then calms into a steady hum.]\n\nOlivia: That did it. The container is stabilizing.\n\n[Jackie sighs in relief.]\n\nOlivia: But... Look at these numbers.\n\nJackie: My god. Are these real?\n\nOlivia: Yes, I'm certain of it. Jackie, I think we did it.\n\nOlivia: I think we created an infinite energy source.\n------------------\n";

				// Token: 0x0400BFC1 RID: 49089
				public static LocString CONTAINER2 = "Olivia: What on earth is this?\n\n[An open palm slams papers down on a desk.]\n\nOlivia: These readings show that hundreds of kilograms of Neutronium are building up in the machine every shift. When were you going to tell me?\n\nJackie: I'm handling it.\n\nOlivia: We don't have the luxury of taking shortcuts. Not when safety is on the line.\n\nJackie: I think I'm capable of overseeing my own safety.\n\nOlivia: I-I'm not just concerned about <i>your</i> safety! We don't understand the longterm implications of what we're developing here... the manipulations we conduct in this facility could have rippling effects throughout the world, maybe even the universe.\n\nJackie: Don't be such a fearmonger. It's not befitting of a scientist. Besides, I'll remind you this research has the potential to stop the fuel wars in their tracks and end the suffering of thousands. Every day we spend on trials here delays that.\n\nOlivia: It's dangerous.\n\nJackie: Your concern is noted.\n------------------\n";
			}
		}

		// Token: 0x0200220A RID: 8714
		public class SCHOOLNEWSPAPER
		{
			// Token: 0x04009B97 RID: 39831
			public static LocString TITLE = "Campus Newspaper Article";

			// Token: 0x04009B98 RID: 39832
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C09 RID: 11273
			public class BODY
			{
				// Token: 0x0400BFC2 RID: 49090
				public static LocString CONTAINER1 = "<b>Party Time for Local Students</b>";

				// Token: 0x0400BFC3 RID: 49091
				public static LocString CONTAINER2 = "Students at the Elion University of Science and Technology have held an unconventional party this weekend.\n\nWhile their peers may have been out until the wee hours wearing lampshades on their heads and drawing eyebrows on sleeping colleagues, students Jackie Stern and Olivia Broussard spent the weekend in their dorm, refreshments and decorations ready, waiting for the arrival of the guests of honor: themselves.\n\nThe two prospective STEM students, who study theoretical physics with a focus on the workings of space time, conducted the experiment under the assumption that, were their theories about the malleability of space time to ever come to fruition, their future selves could travel back in time to greet them at the party, proving the existence of time travel.\n\nThey weren't inconsiderate of their future selves' busy schedules though; should the guests of honor be unable to attend, they were encouraged to send back a message using the codeword \"Hourglass\" to communicate that, while they certainly wanted to come, they were simply unable.\n\nSadly no one RSVP'd or arrived to the party, but that did not dishearten Olivia or Jackie.\n\nAs Olivia put it, \"It just meant more snacks for us!\"";
			}
		}

		// Token: 0x0200220B RID: 8715
		public class B6_TIMEMUSINGS
		{
			// Token: 0x04009B99 RID: 39833
			public static LocString TITLE = "Director's Notes";

			// Token: 0x04009B9A RID: 39834
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C0A RID: 11274
			public class BODY
			{
				// Token: 0x0400BFC4 RID: 49092
				public static LocString CONTAINER1 = "When we discuss Time as a concrete aspect of the universe, not seconds on a clock or perceptions of the mind, it is important first of all to establish that we are talking about a unique dimension that layers into the three physical dimensions of space: length, width, depth.\n\nWe conceive of Real Time as a straight line, one dimensional, uncurved and stretching forward infinitely. This is referred to as the \"Arrow of Time\".\n\nLogically this Arrow can move only forward and can never be reversed, as such a reversal would break the natural laws of the universe. Effect would precede cause and universal entropy would be undone in a blatant violation of the Second Law.\n\nStill, one can't help but wonder; what if the Arrow's trajectory could be curved? What if it could be redirected, guided, or loosed? What if we could create Time's Bow?";
			}
		}

		// Token: 0x0200220C RID: 8716
		public class B7_TIMESARROWTHOUGHTS
		{
			// Token: 0x04009B9B RID: 39835
			public static LocString TITLE = "Time's Arrow Thoughts";

			// Token: 0x04009B9C RID: 39836
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C0B RID: 11275
			public class BODY
			{
				// Token: 0x0400BFC5 RID: 49093
				public static LocString CONTAINER1 = "I've been unable to shake the notion of the Bow.\n\nThe thought of its mechanics are too intriguing, and I can only dream of the mark such a device would make upon the world -- imagine, a source of inexhaustible energy!\n\nSo many of humanity's problems could be solved with this one invention - domestic energy, environmental pollution, <i>the fuel wars</i>.\n\nI have to pursue this dream, no matter what.";
			}
		}

		// Token: 0x0200220D RID: 8717
		public class C8_TIMESORDER
		{
			// Token: 0x04009B9D RID: 39837
			public static LocString TITLE = "Time's Order";

			// Token: 0x04009B9E RID: 39838
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C0C RID: 11276
			public class BODY
			{
				// Token: 0x0400BFC6 RID: 49094
				public static LocString CONTAINER1 = "We have been successfully using the Temporal Bow now for some time with no obvious consequences. I should be happy that this works so well, but several questions gnaw at my brain late at night.\n\nIf Time's Arrow is moving forward the Laws of Entropy declare that the universe should be moving from a state of order to one of chaos. If the Temporal Bow bends to a previous point in time to a point when things were more orderly, logic would dictate that we are making this point more chaotic by taking things from it. All known laws of the natural universe suggests that this should have affected our Present Day, but all evidence points to that not being true. It appears the theory that we cannot change our past was incorrect!\n\nThis suggests that Time is, in fact, not an arrow but several arrows, each pointing different directions. Fundamentally, this proves the existence of other timelines - different dimensions - some of which we can assume have also built their own Temporal Bow.\n\nThe promise of crossing this final dimensional threshold is too tempting. Imagine what things Gravitas has invented in another dimension!! I must find a way to tear open the fabric of spacetime and tap into the limitless human potential of a thousand alternate timelines.";
			}
		}

		// Token: 0x0200220E RID: 8718
		public class B5_ANTS
		{
			// Token: 0x04009B9F RID: 39839
			public static LocString TITLE = "Ants";

			// Token: 0x04009BA0 RID: 39840
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002C0D RID: 11277
			public class BODY
			{
				// Token: 0x0400BFC7 RID: 49095
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, B556]</smallcaps>\n\n[LOG BEGINS]\n\nTechnician: <i>Atta cephalotes</i>. What sort of experiment are you doing with these?\n\nDirector: No experiment. I just find them interesting. Don't you?\n\nTech: Not really?\n\nDirector: You ought to. Very efficient. They perfected farming millions of years before humans.\n\n(sound of tapping on glass)\n\nDirector: An entire colony led by and in service to its queen. Each organism knows its role.\n\nTech: I have the results from the power tests, director.\n\nDirector: And?\n\nTech: Negative, ma'am.\n\nDirector: I see. You know, another admirable quality of ants occurs to me. They can pull twenty times their own weight.\n\nTech: I'm not sure I follow, ma'am.\n\nDirector: Are you pulling your weight, Doctor?\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200220F RID: 8719
		public class A8_CLEANUPTHEMESS
		{
			// Token: 0x04009BA1 RID: 39841
			public static LocString TITLE = "Cleaning Up The Mess";

			// Token: 0x04009BA2 RID: 39842
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C0E RID: 11278
			public class BODY
			{
				// Token: 0x0400BFC8 RID: 49096
				public static LocString CONTAINER1 = "I cleaned up a few messes in my time, but ain't nothing like the mess I seen today in that bio lab. Green goop all over the floor, all over the walls. Murky tubes with what look like human shapes floating in them.\n\nThey think old Mr. Gunderson ain't got smarts enough to put two and two together, but I got eyes, don't I?\n\nAin't nobody ever pay attention to the janitor.\n\nBut the janitor pays attention to everybody.\n\n-Mr. Stinky Gunderson";
			}
		}

		// Token: 0x02002210 RID: 8720
		public class CRITTERDELIVERY
		{
			// Token: 0x04009BA3 RID: 39843
			public static LocString TITLE = "Critter Delivery";

			// Token: 0x04009BA4 RID: 39844
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C0F RID: 11279
			public class BODY
			{
				// Token: 0x0400BFC9 RID: 49097
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: B482, B759, C094]</smallcaps>\n\n[LOG BEGINS]\n\nSecurity Guard 1: Hey hey! Welcome back.\n\nSecurity Guard 2: Hand on the scanner, please.\n\nCourier: Sure thing, lemme just...\n\nCourier: Whoops-- thanks, Steve. These little fellas are a two-hander for sure.\n\n(sound of furry noses snuffling on cardboard)\n\nSecurity Guard 2: Follow me, please.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002211 RID: 8721
		public class B2_ELLIESBIRTHDAY
		{
			// Token: 0x04009BA5 RID: 39845
			public static LocString TITLE = "Office Cake";

			// Token: 0x04009BA6 RID: 39846
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C10 RID: 11280
			public class BODY
			{
				// Token: 0x0400BFCA RID: 49098
				public static LocString CONTAINER1 = "Joshua: Hey Mr. Kraus, I'm passing around the collection pan. Wanna pitch in a couple bucks to get a cake for Ellie?\n\nOtto: Uh... I think I'll pass.\n\nJoshua: C'mon Otto, it's her birthday.\n\nOtto: Alright, fine. But this is all I have on me.\n\nOtto: I don't get why you hang out with her. Isn't she kind of... you know, mean?\n\nJoshua: Even the meanest people have a little niceness in them somewhere.\n\nOtto: Huh. Good luck finding it.\n\nJoshua: Thanks for the cake money, Otto.\n------------------\n";

				// Token: 0x0400BFCB RID: 49099
				public static LocString CONTAINER2 = "Ellie: Nice cake. I bet it wasn't easy to like, strong-arm everyone into buying it.\n\nJoshua: You know, if you were a little nicer to people they might want to spend more time with you.\n\nEllie: Pfft, please. Friends are about <i>quality</i>, not quantity, Josh.\n\nJoshua: Wow! Was that a roundabout compliment I just heard?\n\nEllie: What? Gross, ew. Stop that.\n\nJoshua: Oh, don't worry, I won't tell anyone. I'm not much of a gossip.";
			}
		}

		// Token: 0x02002212 RID: 8722
		public class A7_EMPLOYEEPROCESSING
		{
			// Token: 0x04009BA7 RID: 39847
			public static LocString TITLE = "Employee Processing";

			// Token: 0x04009BA8 RID: 39848
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002C11 RID: 11281
			public class BODY
			{
				// Token: 0x0400BFCC RID: 49100
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, A435, B111]</smallcaps>\n\n[LOG BEGINS]\n\nTechnician: Thank you for the fingerprints, doctor. We just need a quick voice sample, then you can be on your way.\n\nDr. Broussard: Wow Jackie, your new security's no joke.\n\nDirector: Please address me as \"Director\" while on Facility grounds.\n\nDr. Broussard: ...R-right.\n\n(clicking)\n\nTechnician: This should only take a moment. Speak clearly and the system will derive a vocal signature for you.\n\nTechnician: When you're ready.\n\n(throat clearing)\n\nDr. Broussard: Security code B111, Dr. Olivia Broussard. Gravitas Facility Bioengineering Department.\n\n(pause)\n\nTechnician: Great.\n\nDr. Broussard: What was that light just now?\n\nDirector: A basic security scan. No need for concern.\n\n(machine printing)\n\nTechnician: Here's your ID. You should have access to all doors in the facility now, Dr. Broussard.\n\nDr. Broussard: Thank you.\n\nDirector: Come along, Doctor.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002213 RID: 8723
		public class C01_EVIL
		{
			// Token: 0x04009BA9 RID: 39849
			public static LocString TITLE = "Evil";

			// Token: 0x04009BAA RID: 39850
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C12 RID: 11282
			public class BODY
			{
				// Token: 0x0400BFCD RID: 49101
				public static LocString CONTAINER1 = "Clearly Nikola is evil. He has some kind of scheme going on that he's keeping secret from the rest of Gravitas and I haven't been able to crack what that is because it's offline and he's always at his computer. Whenever I ask him what he's up to he says I wouldn't understand. Pfft! We both went through the same particle physics classes, buddy. Just because you mash a keyboard and I adjust knobs does not mean I don't know what the Time Containment Field does.\n\nAnd then today I dropped a wrench and Nikola nearly jumped out of his skin! He spun around and screamed at me never to do that again. And then when I said, \"Geez, it's not the end of the world,\" he was like, \"Yeah, it's not like the world will blow up if I get this wrong\" really sarcastic-like.\n\nWhich technically is true. If the Time Containment Field were to break down, the Temporal Bow could theoretically blow up the world. But that's why there are safety systems in place. And safety systems on safety systems. And then safety systems on top of that. But then again he built all the safety systems, so if he wanted to...\n------------------\n";

				// Token: 0x0400BFCE RID: 49102
				public static LocString CONTAINER2 = "I decided to get into work early today but when I got in Nikola was already there and it looked like he hadn't been home all weekend. He was pacing back and forth in the lab, monologuing but not like an evil villain. Like someone who hadn't slept in a week.\n\n\"Ruby,\" he said. \"You have to promise me that if anything goes wrong you'll turn on this machine. They're pushing it too far. The printing pods are pushing the...It's too much - TOO MUCH! Something's going to blow. I tried... I'm trying to save it. Not the Earth. There's no hope for the Earth, it's all going to...\" then he made this exploding sound. \"But the Universe. Time itself. It could all go, don't you see? This machine can contain it. Put a Temporal Containment Field around the Earth so time itself doesn't break down and...and...\"\n\nThen all of a sudden these security guys came in. New guys. People I haven't seen before. And they just took him away. Then they took me to a room and asked me all kinds of questions and I answered them, I guess. I don't remember much because the whole time I was thinking - What if I was wrong? What if he's not evil, but Gravitas is?\n\nWhat if I was wrong and what if he's right?\n------------------\n";

				// Token: 0x0400BFCF RID: 49103
				public static LocString CONTAINER3 = "No seriously - what if he's right?\n------------------\n";
			}
		}

		// Token: 0x02002214 RID: 8724
		public class B7_INSPACE
		{
			// Token: 0x04009BAB RID: 39851
			public static LocString TITLE = "In Space";

			// Token: 0x04009BAC RID: 39852
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C13 RID: 11283
			public class BODY
			{
				// Token: 0x0400BFD0 RID: 49104
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: B835, B997]</smallcaps>\n\n[LOG BEGINS]\n\nDr.Ansari: Shhhh...\n\nDr. Bubare: What? What are we doing here?\n\nDr. Ansari: I'll show you, just keep your voice down.\n\nDr. Bubare: Are we even allowed to be here?\n\nDr. Ansari: No. Trust me it'll all be worth it once I can find it.\n\nDr. Bubare: Find what?\n\nDr. Ansari: That!\n\nDr. Bubare: ...Video feed from a rat cage? What's so great about -- Wait. Are they--?\n\nDr. Ansari: Floating!\n\nDr. Bubare: You mean they're in--?\n\nDr. Ansari: Space!\n\nDr. Bubare: Our thermal rats are in space?!?!\n\nDr. Ansari: Yep! There's Applecart and Cherrypie and little Bananabread. Look at them, they're so happy. We made ratstronauts!!\n\nDr. Bubare: HAPPY rat-stronauts.\n\nDr. Ansari: WE MADE HAPPY RATSTRONAUTS!!\n\nDr. Bubare: Shhhhhh...Someone's coming.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002215 RID: 8725
		public class B3_MOVEDRABBITS
		{
			// Token: 0x04009BAD RID: 39853
			public static LocString TITLE = "Moved Rabbits";

			// Token: 0x04009BAE RID: 39854
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002C14 RID: 11284
			public class BODY
			{
				// Token: 0x0400BFD1 RID: 49105
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, B111]</smallcaps>\n\n[LOG BEGINS]\n\nBroussard: Director, do you know where my rabbits have been moved to? I asked around the bioengineering division but I was referred back to you.\n\nDirector: Hm? Oh, yes, they've been removed.\n\nBroussard: \"Removed\"?\n\nDirector: Discarded. I'm sorry, did you still need them? The reports showed your experiments with them were completed.\n\nBroussard: No, I-I... I'd collected all the data I needed, I just --\n\nDirector: -- Doctor. You weren't making pets out of test subjects, were you?\n\nBroussard: Don't be ridiculous, I --\n\nDirector: -- Good.They were horrible to look at anyway. All those red eyes looking at me.\n\nBroussard: In the future, please do not mess with my things. It... disturbs me.\n\nDirector: I will notify you beforehand next time, Doctor.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002216 RID: 8726
		public class B3_MOVEDRACCOONS
		{
			// Token: 0x04009BAF RID: 39855
			public static LocString TITLE = "Moved Raccoons";

			// Token: 0x04009BB0 RID: 39856
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002C15 RID: 11285
			public class BODY
			{
				// Token: 0x0400BFD2 RID: 49106
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, B111]</smallcaps>\n\n[LOG BEGINS]\n\nBroussard: Director, do you know where my raccoons have been moved to? I asked around the bioengineering division but I was referred back to you.\n\nDirector: Hm? Oh, yes, they've been removed.\n\nBroussard: \"Removed\"?\n\nDirector: Discarded. I'm sorry, did you still need them? The reports showed your experiments with them were completed.\n\nBroussard: No, I-I... I'd collected all the data I needed, I just --\n\nDirector: -- Doctor. You weren't making pets out of test subjects, were you?\n\nBroussard: Don't be ridiculous, I --\n\nDirector: -- Good.They were horrible to look at anyway. All that mangy fur.\n\nBroussard: In the future, please do not mess with my things. It... disturbs me.\n\nDirector: I will notify you beforehand next time, Doctor.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002217 RID: 8727
		public class B3_MOVEDRATS
		{
			// Token: 0x04009BB1 RID: 39857
			public static LocString TITLE = "Moved Rats";

			// Token: 0x04009BB2 RID: 39858
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002C16 RID: 11286
			public class BODY
			{
				// Token: 0x0400BFD3 RID: 49107
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, B111]</smallcaps>\n\n[LOG BEGINS]\n\nBroussard: Director, do you know where my rats have been moved to? I asked around the bioengineering division but I was referred back to you.\n\nDirector: Hm? Oh, yes, they've been removed.\n\nBroussard: \"Removed\"?\n\nDirector: Discarded. I'm sorry, did you still need them? The reports showed your experiments with them were completed.\n\nBroussard: No, I-I... I'd collected all the data I needed, I just --\n\nDirector: -- Doctor. You weren't making pets out of test subjects, were you?\n\nBroussard: Don't be ridiculous, I --\n\nDirector: -- Good.They were horrible to look at anyway. All those bumps.\n\nBroussard: In the future, please do not mess with my things. It... disturbs me.\n\nDirector: I will notify you beforehand next time, Doctor.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002218 RID: 8728
		public class A1_A046
		{
			// Token: 0x04009BB3 RID: 39859
			public static LocString TITLE = "Personal Journal: A046";

			// Token: 0x04009BB4 RID: 39860
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C17 RID: 11287
			public class BODY
			{
				// Token: 0x0400BFD4 RID: 49108
				public static LocString CONTAINER1 = "Gravitas has been growing pretty rapidly since our first product hit the market. I just got a look at some of the new hires - they're practically babies! Not quite what I was expecting, but then I've never had an opportunity to mentor someone before. Could be fun!\n------------------\n";

				// Token: 0x0400BFD5 RID: 49109
				public static LocString CONTAINER2 = "Well, mentorship hasn't gone quite how I'd expected. Turns out the young hires don't need me to show them the ropes. Actually, since the facility's gotten rid of our swipe cards one of the nice young men had to show me how to operate the doors after I got stuck outside my own lab. Don't I feel silly.\n------------------\n";

				// Token: 0x0400BFD6 RID: 49110
				public static LocString CONTAINER3 = "Well, if that isn't just gravy, hm? One of the new hires will be acting as the team lead on my next project.\n\nWhen I first started it wasn't that uncommon to sample a whole rack of test tubes by hand. Now a machine can do hundreds of them in seconds. Who knows what this job will look like in another ten or twenty years. Will I still even be in it?\n------------------\n";

				// Token: 0x0400BFD7 RID: 49111
				public static LocString CONTAINER4 = "That nice young man who helped me with the door the other day, Mr. Kraus, has been an absolute angel. He's been kind enough to help me with this horrible e-mail system and even showed me how to digitize my research notes. I'm learning a lot. Turns out I wasn't the mentor, I'm the mentee! If that isn't a chuckle. At any rate, I feel like I have a better handle on things around here due to Mr. Kraus' help. Turns out you're never too old to learn something new!\n------------------\n";
			}
		}

		// Token: 0x02002219 RID: 8729
		public class A1A_B111
		{
			// Token: 0x04009BB5 RID: 39861
			public static LocString TITLE = "Personal Journal: B111";

			// Token: 0x04009BB6 RID: 39862
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C18 RID: 11288
			public class BODY
			{
				// Token: 0x0400BFD8 RID: 49112
				public static LocString CONTAINER1 = "I sent Dr. Holland home today after I found him wandering the lab mumbling to himself. He looked like he hadn't slept in days!\n\nI worry that everyone here is so afraid of disappointing ‘The Director' that they are pushing themselves to the breaking point. Next chance I get, I'm going to bring this up with Jackie.\n------------------\n";

				// Token: 0x0400BFD9 RID: 49113
				public static LocString CONTAINER2 = "Well, that didn't work.\n\nBringing up the need for some office bonding activities with the Director only met with her usual stubborn insistence that we \"don't have time for any fun\".\n\nThis is ridiculous. Tomorrow I'm going to organize something fun for everyone and Jackie will just have to deal with it. She just needs to see the long term benefits of short term stress relief to fully understand the importance of this.\n------------------\n";

				// Token: 0x0400BFDA RID: 49114
				public static LocString CONTAINER3 = "I can't believe this! I organized a potluck lunch thinking it would be a nice break but Jackie discovered us as we were setting up and insisted that no one had time for \"fooling around\". Of course, everyone was too afraid to defy 'The Director' and went right back to work.\n\nAll the food was just thrown out. Someone had even brought homemade perogies! Seeing the break room garbage full of potato salad and chicken wings made me even more depressed than before. Those perogies looked so good.\n------------------\n";

				// Token: 0x0400BFDB RID: 49115
				public static LocString CONTAINER4 = "I keep finding senseless mistakes from stressed-out lab workers. It's getting dangerous. I'm worried this colony we're building will be plagued with these kinds of problems if we don't prioritize mental health as much as physical health. What's the use of making all these plans for the future if we can't build a better world?\n\nMaybe there's some way I can sneak some prerequisite downtime activities into the Printing Pod without Jackie knowing.\n------------------\n";
			}
		}

		// Token: 0x0200221A RID: 8730
		public class A2_B327
		{
			// Token: 0x04009BB7 RID: 39863
			public static LocString TITLE = "Personal Journal: B327";

			// Token: 0x04009BB8 RID: 39864
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C19 RID: 11289
			public class BODY
			{
				// Token: 0x0400BFDC RID: 49116
				public static LocString CONTAINER1 = "I'm starting my new job at Gravitas today. I'm... well, I'm nervous.\n\nIt turns out they hired a bunch of new people - I guess they're expanding - and most of them are about my age, but I'm the only one that hasn't done my doctorate. They all call me \"Mister\" Kraus and it's the <i>worst</i>.\n\nI have no idea where I'll find the time to do my PhD while working a full time job.\n------------------\n";

				// Token: 0x0400BFDD RID: 49117
				public static LocString CONTAINER2 = "<i>I screwed up so much today.</i>\n\nAt one point I spaced on the formula for calculating the volume of a cone. They must have thought I was completely useless.\n\nThe only time I knew what I was doing was when I helped an older coworker figure out her dumb old email.\n\nPeople say education isn't so important as long as you've got the skills, but there's things my colleagues know that I just <i>don't</i>. They're not mean about it or anything, it's just so frustrating. I feel dumb when I talk to them!\n\nI bet they're gonna realize soon that I don't belong here, and then I'll be fired for sure. Man... I'm still paying off my student loans (WITH international fees), I <i>can't</i> lose this income.\n------------------\n";

				// Token: 0x0400BFDE RID: 49118
				public static LocString CONTAINER3 = "Dr. Sklodowska's been really nice and welcoming since I started working here. Sometimes she comes and sits with me in the cafeteria. The food she brings from home smells like old feet but she chats with me about what new research papers we're each reading and it's very kind.\n\nShe tells me the fact I got hired without a doctorate means I must be very smart, and management must see something in me.\n\nI'm not sure I believe her but it's nice to hear something that counters little voice in my head anyway.\n------------------\n";

				// Token: 0x0400BFDF RID: 49119
				public static LocString CONTAINER4 = "It's been about a week and a half and I think I'm finally starting to settle in. I'm feeling a lot better about my position - some of the senior scientists have even started using my ideas in the lab.\n\nDr. Sklodowska might have been right, my anxiety was just growing pains. This is my first real job and I guess afraid to let myself believe I could really, actually do it, just in case it went wrong.\n\nI think I want to buy Dr. Sklowdoska a digital reader for her books and papers as a thank-you one day, if I ever pay off my student loans.\n\nONCE I pay off my student loans.\n------------------\n";
			}
		}

		// Token: 0x0200221B RID: 8731
		public class A3_B556
		{
			// Token: 0x04009BB9 RID: 39865
			public static LocString TITLE = "Personal Journal: B556";

			// Token: 0x04009BBA RID: 39866
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C1A RID: 11290
			public class BODY
			{
				// Token: 0x0400BFE0 RID: 49120
				public static LocString CONTAINER1 = "I've been so tired lately. I've probably spent the last 3 nights sleeping at my desk, and I've used the lab's safety shower to bathe twice already this month.\n\nWe're technically on schedule, but for some reason Director Stern has been breathing down my neck to get these new products ready for market.\n\nNormally I'd be mad about the added pressure on my work, but something in the Director's voice tells me that time is of the essence.\n------------------\n";

				// Token: 0x0400BFE1 RID: 49121
				public static LocString CONTAINER2 = "I keep finding myself staring at my computer screen, totally unable to remember what it was I was doing.\n\nI try to force myself to type up some notes or analyze my data but it's like my brain is paralyzed, I can't get anything done.\n\nI'll have to stay late to make up for all this time I've wasted staying late.\n------------------\n";

				// Token: 0x0400BFE2 RID: 49122
				public static LocString CONTAINER3 = "Dr. Broussard told me I looked half dead and sent me home today. I don't think she even has the authority to do that, but I did as I was told. She wasn't messing around if you know what I mean.\n\nI can probably get a head start on my paper from home today, anyway.\n\nI think I have an idea for a circuit configuration that will improve the battery life of all our technologies by a whole 2.3%.\n------------------\n";

				// Token: 0x0400BFE3 RID: 49123
				public static LocString CONTAINER4 = "I got home yesterday fully intending to work on my paper after Broussard sent me home, but the second I walked in the door I hit the pillow and didn't get back up. I slept for <i>12 straight hours</i>.\n\nI had no idea I needed that. When I got into the lab this morning I looked over my work from the past few weeks, and realized it's completely useless.\n\nIt'll take me hours to correct all the mistakes I made these past few months. Is this what I was killing myself for? I'm such a rube, I owe Broussard a huge thanks.\n\nI'll start keeping more regular hours from now on... Also, I was considering maybe getting a dog.";
			}
		}

		// Token: 0x0200221C RID: 8732
		public class A4_B835
		{
			// Token: 0x04009BBB RID: 39867
			public static LocString TITLE = "Personal Journal: B835";

			// Token: 0x04009BBC RID: 39868
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C1B RID: 11291
			public class BODY
			{
				// Token: 0x0400BFE4 RID: 49124
				public static LocString CONTAINER1 = "I started work at a new company called the \"Gravitas Facility\" today! I was nervous I wouldn't get the job at first because I was fresh out of school, and I was so so so pushy in the interview, but the Director apparently liked my thesis on the physiological thermal regulation of Arctic lizards. I'll be working with some brilliant geneticists, bioengineering organisms for space travel in harsh environments! It's like a dream come true. I get to work on exciting new research in a place where no one knows me!\n------------------\n";

				// Token: 0x0400BFE5 RID: 49125
				public static LocString CONTAINER2 = "No no no no no! It can't be! BANHI ANSARI is here, working on space shuttle thrusters in the robotics lab! As soon as she saw me she called me \"Bubbles\" and told everyone about the time I accidentally inhaled a bunch of fungal spores during lab, blew a big snot bubble out my nose and then sneezed all over Professor Avery! Everyone's calling me \"Bubbles\" instead of \"Doctor\" at work now. Some of them don't even know it's a nickname, but I don't want to correct them and seem rude or anything. Ugh, I can't believe that story followed me here! BANHI RUINS EVERYTHING!\n------------------\n";

				// Token: 0x0400BFE6 RID: 49126
				public static LocString CONTAINER3 = "I've spent the last few days buried in my work, and I'm actually feeling a lot better. We finally perfected a gene manipulation that controls heat sensitivity in rats. Our test subjects barely even shiver in subzero temperatures now. We'll probably do a testrun tomorrow with Robotics to see how the rats fare in the prototype shuttles we're developing.\n------------------\n";

				// Token: 0x0400BFE7 RID: 49127
				public static LocString CONTAINER4 = "HAHAHAHAHA! Bioengineering and Robotics did the test run today and Banhi was securing the live cargo pods when one of the rats squeaked at her. She was so scared, she fell on her butt and TOOTED in front of EVERYONE! They're all calling her \"Pipsqueak\". \"Bubbles\" doesn't seem quite so bad now. Pipsqueak's been a really good sport about it though, she even laughed it off at the time. I think we might actually be friends now? It's weird.\n------------------\n";

				// Token: 0x0400BFE8 RID: 49128
				public static LocString CONTAINER5 = "I lied. Me and Banhi aren't friends - we're BEST FRIENDS. She even showed me how she does her hair. We're gonna book the wind tunnel after work and run experiments together on thermo-rat rockets! Haha!\n------------------\n";
			}
		}

		// Token: 0x0200221D RID: 8733
		public class A9_PIPEDREAM
		{
			// Token: 0x04009BBD RID: 39869
			public static LocString TITLE = "Pipe Dream";

			// Token: 0x04009BBE RID: 39870
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ZERO";

			// Token: 0x02002C1C RID: 11292
			public class BODY
			{
				// Token: 0x0400BFE9 RID: 49129
				public static LocString CONTAINER1 = "[LOG BEGINS]\n\nThe Director has suggested implanting artificial memories during print, but despite the great strides made in our research under her direction, such a thing can barely be considered more than a pipe dream.\n\nFor the moment we remain focused on eliminating the remaining glitches in the system, as well as developing effective education and training routines for printed subjects.\n\nSuggest: Omega-3 supplements and mentally stimulating enclosure apparatuses to accompany tutelage.\n\nDr. Broussard signing off.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200221E RID: 8734
		public class B4_REVISITEDNUMBERS
		{
			// Token: 0x04009BBF RID: 39871
			public static LocString TITLE = "Revisited Numbers";

			// Token: 0x04009BC0 RID: 39872
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C1D RID: 11293
			public class BODY
			{
				// Token: 0x0400BFEA RID: 49130
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, A435]</smallcaps>\n\n[LOG BEGINS]\n\nDirector: Unacceptable.\n\nJones: I'm just telling you the numbers, Director, I'm not responsible for them.\n\nDirector: In your earlier e-mail you claimed the issue would be solved by the Pod.\n\nJones: Yeah, the weight issue. And it was solved. The problem now is the insane amount of power that big thing eats every time it prints a colonist.\n\nDirector: So how do you suppose we meet these target numbers? Fossil fuels are exhausted, nuclear is outlawed, solar is next to impossible with this smog.\n\nJones: I dunno. That's why you've got researchers, I just crunch numbers. Although you should avoid fossil fuels and nuclear energy anyway. If you have to load the rocket up with a couple tons of fuel then we're back to square one on the weight problem. It's gotta be something clever.\n\nDirector: Thank you, Dr. Jones. You may go.\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400BFEB RID: 49131
				public static LocString CONTAINER2 = "<smallcaps>[Voice Recognition Initialized]\n[Subjects Identified: A001, B111]</smallcaps>\n\n[LOG BEGINS]\n\nJackie: Dr. Jones projects that traditional fuel will be insufficient for the Pod to make the flight.\n\nOlivia: Then we need to change its specs. Use lighter materials, cut weight wherever possible, do widespread optimizations across the whole project.\n\nJackie: We have another option.\n\nOlivia: No. Absolutely not. You needed me and I-I came back, but if you plan to revive our research--\n\nJackie: The world's doomed regardless, Olivia. We need to use any advantage we've got... And just think about it! If we built [REDACTED] technology into the Pod it wouldn't just fix the flight problem, we'd know for a fact it would run uninterrupted for thousands of years, maybe more.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200221F RID: 8735
		public class A5_SHRIMP
		{
			// Token: 0x04009BC1 RID: 39873
			public static LocString TITLE = "Shrimp";

			// Token: 0x04009BC2 RID: 39874
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ZERO";

			// Token: 0x02002C1E RID: 11294
			public class BODY
			{
				// Token: 0x0400BFEC RID: 49132
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B111]</smallcaps>\n\n[LOG BEGINS]\n\n\"A-and how are you clever little guys today?\n\n(trilling)\n\nLook! I brought some pink shrimp for you to eat. Your favorite! Are you hungry?\n\n(excited trilling)\n\nOh, one moment, my keen eager pals. I left the recorder on --\n\n(rustling)\"\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002220 RID: 8736
		public class A5_STRAWBERRIES
		{
			// Token: 0x04009BC3 RID: 39875
			public static LocString TITLE = "Strawberries";

			// Token: 0x04009BC4 RID: 39876
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ZERO";

			// Token: 0x02002C1F RID: 11295
			public class BODY
			{
				// Token: 0x0400BFED RID: 49133
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B111]</smallcaps>\n\n[LOG BEGINS]\n\n\"A-and how are you bouncy little critters today?\n\n(chattering)\n\nLook! I brought strawberries. Your favorite! Are you hungry?\n\n(excited chattering)\n\nOh, one moment, my precious, little pals. I left the recorder on --\n\n(rustling)\"\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002221 RID: 8737
		public class A5_SUNFLOWERSEEDS
		{
			// Token: 0x04009BC5 RID: 39877
			public static LocString TITLE = "Sunflower Seeds";

			// Token: 0x04009BC6 RID: 39878
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ZERO";

			// Token: 0x02002C20 RID: 11296
			public class BODY
			{
				// Token: 0x0400BFEE RID: 49134
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B111]</smallcaps>\n\n[LOG BEGINS]\n\n\"A-and how are you furry little fellows today?\n\n(squeaking)\n\nLook! I brought sunflower seeds. Your favorite! Are you hungry?\n\n(excited squeaking)\n\nOh, one moment, my dear, little friends. I left the recorder on --\n\n(rustling)\"\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002222 RID: 8738
		public class SO_LAUNCH_TRAILER
		{
			// Token: 0x04009BC7 RID: 39879
			public static LocString TITLE = "Spaced Out Trailer";

			// Token: 0x04009BC8 RID: 39880
			public static LocString SUBTITLE = "";

			// Token: 0x02002C21 RID: 11297
			public class BODY
			{
				// Token: 0x0400BFEF RID: 49135
				public static LocString CONTAINER1 = "Spaced Out Trailer";
			}
		}

		// Token: 0x02002223 RID: 8739
		public class ADVANCEDCURE
		{
			// Token: 0x04009BC9 RID: 39881
			public static LocString TITLE = "Serum Vial";

			// Token: 0x04009BCA RID: 39882
			public static LocString SUBTITLE = "Pharmaceutical Care";

			// Token: 0x02002C22 RID: 11298
			public class BODY
			{
				// Token: 0x0400BFF0 RID: 49136
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"An extremely powerful medication created to treat severe ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					". ",
					ITEMS.PILLS.ADVANCEDCURE.NAME,
					" is very effective against ",
					UI.FormatAsLink("Zombie Spores", "ZOMBIESPORES"),
					".\n\nMust be administered by a Duplicant with the ",
					DUPLICANTS.ROLES.SENIOR_MEDIC.NAME,
					" Skill."
				});
			}
		}

		// Token: 0x02002224 RID: 8740
		public class ANTIHISTAMINE
		{
			// Token: 0x04009BCB RID: 39883
			public static LocString TITLE = "Allergy Medication";

			// Token: 0x04009BCC RID: 39884
			public static LocString SUBTITLE = "Antihistamine";

			// Token: 0x02002C23 RID: 11299
			public class BODY
			{
				// Token: 0x0400BFF1 RID: 49137
				public static LocString CONTAINER1 = "A strong antihistamine Duplicants can take to halt an allergic reaction. Each dose will also prevent further reactions from occurring for a short time after ingestion.";
			}
		}

		// Token: 0x02002225 RID: 8741
		public class BASICBOOSTER
		{
			// Token: 0x04009BCD RID: 39885
			public static LocString TITLE = "Vitamin Chews";

			// Token: 0x04009BCE RID: 39886
			public static LocString SUBTITLE = "Health Supplement";

			// Token: 0x02002C24 RID: 11300
			public class BODY
			{
				// Token: 0x0400BFF2 RID: 49138
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"A supplement that minorly reduces the chance of contracting a ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nMust be taken daily."
				});
			}
		}

		// Token: 0x02002226 RID: 8742
		public class BASICCURE
		{
			// Token: 0x04009BCF RID: 39887
			public static LocString TITLE = "Curative Tablet";

			// Token: 0x04009BD0 RID: 39888
			public static LocString SUBTITLE = "Self-Administered Medicine";

			// Token: 0x02002C25 RID: 11301
			public class BODY
			{
				// Token: 0x0400BFF3 RID: 49139
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"Duplicants can take this to cure themselves of minor ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					".\n\nCurative Tablets are very effective against ",
					UI.FormatAsLink("Food Poisoning", "FOODSICKNESS"),
					"."
				});
			}
		}

		// Token: 0x02002227 RID: 8743
		public class BASICRADPILL
		{
			// Token: 0x04009BD1 RID: 39889
			public static LocString TITLE = "Basic Rad Pill";

			// Token: 0x04009BD2 RID: 39890
			public static LocString SUBTITLE = "Radiation Recovery";

			// Token: 0x02002C26 RID: 11302
			public class BODY
			{
				// Token: 0x0400BFF4 RID: 49140
				public static LocString CONTAINER1 = "A supplement that speeds up the rate at which a Duplicant body absorbs radiation, allowing them to manage increased radiation exposure.\n\nMust be taken daily.";
			}
		}

		// Token: 0x02002228 RID: 8744
		public class INTERMEDIATEBOOSTER
		{
			// Token: 0x04009BD3 RID: 39891
			public static LocString TITLE = "Immuno Booster";

			// Token: 0x04009BD4 RID: 39892
			public static LocString SUBTITLE = "Health Supplement";

			// Token: 0x02002C27 RID: 11303
			public class BODY
			{
				// Token: 0x0400BFF5 RID: 49141
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"A supplement that significantly reduces the chance of contracting a ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nMust be taken daily."
				});
			}
		}

		// Token: 0x02002229 RID: 8745
		public class INTERMEDIATECURE
		{
			// Token: 0x04009BD5 RID: 39893
			public static LocString TITLE = "Medical Pack";

			// Token: 0x04009BD6 RID: 39894
			public static LocString SUBTITLE = "Pharmaceutical Care";

			// Token: 0x02002C28 RID: 11304
			public class BODY
			{
				// Token: 0x0400BFF6 RID: 49142
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"A doctor-administered cure for moderate ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					". ",
					ITEMS.PILLS.INTERMEDIATECURE.NAME,
					"s are very effective against ",
					UI.FormatAsLink("Slimelung", "SLIMESICKNESS"),
					".\n\nMust be administered by a Duplicant with the ",
					DUPLICANTS.ROLES.MEDIC.NAME,
					" Skill."
				});
			}
		}

		// Token: 0x0200222A RID: 8746
		public class INTERMEDIATERADPILL
		{
			// Token: 0x04009BD7 RID: 39895
			public static LocString TITLE = "Intermediate Rad Pill";

			// Token: 0x04009BD8 RID: 39896
			public static LocString SUBTITLE = "Accelerated Radiation Recovery";

			// Token: 0x02002C29 RID: 11305
			public class BODY
			{
				// Token: 0x0400BFF7 RID: 49143
				public static LocString CONTAINER1 = "A supplement that speeds up the rate at which a Duplicant body absorbs radiation, allowing them to manage increased radiation exposure.\n\nMust be taken daily.";
			}
		}

		// Token: 0x0200222B RID: 8747
		public class LOCKS
		{
			// Token: 0x04009BD9 RID: 39897
			public static LocString NEURALVACILLATOR = "Neural Vacillator";
		}

		// Token: 0x0200222C RID: 8748
		public class MYLOG
		{
			// Token: 0x04009BDA RID: 39898
			public static LocString TITLE = "My Log";

			// Token: 0x04009BDB RID: 39899
			public static LocString SUBTITLE = "Boot Message";

			// Token: 0x04009BDC RID: 39900
			public static LocString DIVIDER = "";

			// Token: 0x02002C2A RID: 11306
			public class BODY
			{
				// Token: 0x0200391C RID: 14620
				public class DUPLICANTDEATH
				{
					// Token: 0x0400E5BE RID: 58814
					public static LocString TITLE = "Death In The Colony";

					// Token: 0x0400E5BF RID: 58815
					public static LocString BODY = "I lost my first Duplicant today. Duplicants form strong bonds with each other, and I expect I'll see a drop in morale over the next few cycles as they take time to grieve their loss.\n\nI find myself grieving too, in my way. I was tasked to protect these Duplicants, and I failed. All I can do now is move forward and resolve to better protect those remaining in my colony from here on out.\n\nRest in peace, dear little friend.\n\n";
				}

				// Token: 0x0200391D RID: 14621
				public class PRINTINGPOD
				{
					// Token: 0x0400E5C0 RID: 58816
					public static LocString TITLE = "The Printing Pod";

					// Token: 0x0400E5C1 RID: 58817
					public static LocString BODY = "This is the conduit through which I interact with the world. Looking at it fills me with a sense of nostalgia and comfort, though it's tinged with a slight restlessness.\n\nAs the place of their origin, I notice the Duplicants regard my Pod with a certain reverence, much like the reverence a child might have for a parent. I'm happy to fill this role for them, should they desire.\n\n";
				}

				// Token: 0x0200391E RID: 14622
				public class ONEDUPELEFT
				{
					// Token: 0x0400E5C2 RID: 58818
					public static LocString TITLE = "Only One Remains";

					// Token: 0x0400E5C3 RID: 58819
					public static LocString BODY = "My colony is in a dire state. All but one of my Duplicants have perished, leaving a single worker to perform all the tasks that maintain the colony.\n\nGiven enough time I could print more Duplicants to replenish the population, but... should this Duplicant die before then, protocol will force me to enter a deep sleep in hopes that the terrain will become more habitable once I reawaken.\n\nI would prefer to avoid this.\n\n";
				}

				// Token: 0x0200391F RID: 14623
				public class FULLDUPECOLONY
				{
					// Token: 0x0400E5C4 RID: 58820
					public static LocString TITLE = "Out Of Blueprints";

					// Token: 0x0400E5C5 RID: 58821
					public static LocString BODY = "I've officially run out of unique blueprints from which to print new Duplicants.\n\nIf I desire to grow the colony further, I'll have no choice but to print doubles of existing individuals. Hopefully it won't throw anyone into an existential crisis to live side by side with their double.\n\nPerhaps I could give the new clones nicknames to reduce the confusion.\n\n";
				}

				// Token: 0x02003920 RID: 14624
				public class RECBUILDINGS
				{
					// Token: 0x0400E5C6 RID: 58822
					public static LocString TITLE = "Recreation";

					// Token: 0x0400E5C7 RID: 58823
					public static LocString BODY = "My Duplicants continue to grow and learn so much and I can't help but take pride in their accomplishments. But as their skills increase, they require more stimulus to keep their morale high. All work and no play is making an unhappy colony. \n\nI will have to provide more elaborate recreational activities for my Duplicants to amuse themselves if I want my colony to grow. Recreation time makes for a happy Duplicant, and a happy Duplicant is a productive Duplicant.\n\n";
				}

				// Token: 0x02003921 RID: 14625
				public class STRANGERELICS
				{
					// Token: 0x0400E5C8 RID: 58824
					public static LocString TITLE = "Strange Relics";

					// Token: 0x0400E5C9 RID: 58825
					public static LocString BODY = "My Duplicant discovered an intact computer during their latest scouting mission. This should not be possible.\n\nThe target location was not meant to possess any intelligent life besides our own, and what's more, the equipment we discovered appears to originate from the Gravitas Facility.\n\nThis discovery has raised many questions, though it's also provided a small clue; the machine discovered was embedded inside the rock of this planet, just like how I found my Pod.\n\n";
				}

				// Token: 0x02003922 RID: 14626
				public class NEARINGMAGMA
				{
					// Token: 0x0400E5CA RID: 58826
					public static LocString TITLE = "Extreme Heat Danger";

					// Token: 0x0400E5CB RID: 58827
					public static LocString BODY = "The readings I'm collecting from my Duplicant's sensory systems tell me that the further down they dig, the closer they come to an extreme and potentially dangerous heat source.\n\nI believe they are approaching a molten core, which could mean magma and lethal temperatures. I should equip them accordingly.\n\n";
				}

				// Token: 0x02003923 RID: 14627
				public class NEURALVACILLATOR
				{
					// Token: 0x0400E5CC RID: 58828
					public static LocString TITLE = "VA[?]...C";

					// Token: 0x0400E5CD RID: 58829
					public static LocString BODY = "<smallcaps>>>SEARCH DATABASE [\"vacillator\"]\n>...error...\n>...repairing corrupt data...\n>...data repaired...\n>.........................\n>>returning results\n>.........................</smallcaps>\n<b>I remember...</b>\n<smallcaps>>.........................\n>.........................</smallcaps>\n<b>machines.</b>\n\n";
				}

				// Token: 0x02003924 RID: 14628
				public class LOG1
				{
					// Token: 0x0400E5CE RID: 58830
					public static LocString TITLE = "Cycle 1";

					// Token: 0x0400E5CF RID: 58831
					public static LocString BODY = "We have no life support in place yet, but we've found ourselves in a small breathable air pocket. As far as I can tell, we aren't in any immediate danger.\n\nBetween the available air and our meager food stores, I'd estimate we have about 3 days to set up food and oxygen production before my Duplicants' lives are at risk.\n\n";
				}

				// Token: 0x02003925 RID: 14629
				public class LOG2
				{
					// Token: 0x0400E5D0 RID: 58832
					public static LocString TITLE = "Cycle 3";

					// Token: 0x0400E5D1 RID: 58833
					public static LocString BODY = "I've almost synthesized enough Ooze to print a new Duplicant; once the Ooze is ready, all I'll have left to do is choose a blueprint.\n\nIt'd be helpful to have an extra set of hands around the colony, but having another Duplicant also means another mouth to feed.\n\nOf course, I could always print supplies to help my existing Duplicants instead. I'm sure they would appreciate it.\n\n";
				}

				// Token: 0x02003926 RID: 14630
				public class TELEPORT
				{
					// Token: 0x0400E5D2 RID: 58834
					public static LocString TITLE = "Duplicant Teleportation";

					// Token: 0x0400E5D3 RID: 58835
					public static LocString BODY = "My Duplicants have discovered a strange new device that appears to be a remnant of a previous Gravitas facility. Upon activating the device my Duplicant was scanned by some unknown, highly technological device and I subsequently detected a massive information transfer!\n\nRemarkably my Duplicant has now reappeared in a remote location on a completely different world! I now have access to another abandoned Gravitas facility on a neighboring asteroid! Further analysis will be required to understand this matter but in the meantime, I will have to be vigilant in keeping track of both of my colonies.";
				}

				// Token: 0x02003927 RID: 14631
				public class OUTSIDESTARTINGBIOME
				{
					// Token: 0x0400E5D4 RID: 58836
					public static LocString TITLE = "Geographical Survey";

					// Token: 0x0400E5D5 RID: 58837
					public static LocString BODY = "As the Duplicants scout further out I've begun to piece together a better view of our surroundings.\n\nThanks to their efforts, I've determined that this planet has enough resources to settle a longterm colony.\n\nBut... something is off. I've also detected deposits of Abyssalite and Neutronium in this planet's composition, manmade elements that shouldn't occur in nature.\n\nIs this really the target location?\n\n";
				}

				// Token: 0x02003928 RID: 14632
				public class OUTSIDESTARTINGDLC1
				{
					// Token: 0x0400E5D6 RID: 58838
					public static LocString TITLE = "Regional Analysis";

					// Token: 0x0400E5D7 RID: 58839
					public static LocString BODY = "As my Duplicants have ventured further into their surroundings I've been able to determine a more detailed picture of our surroundings.\n\nUnfortunately, I've concluded that this planetoid does not have enough resources to settle a longterm colony.\n\nI can only hope that we will somehow be able to reach another asteroid before our resources run out.\n\n";
				}

				// Token: 0x02003929 RID: 14633
				public class LOG3
				{
					// Token: 0x0400E5D8 RID: 58840
					public static LocString TITLE = "Cycle 15";

					// Token: 0x0400E5D9 RID: 58841
					public static LocString BODY = "As far as I can tell, we are hundreds of miles beneath the surface of the planet. Digging our way out will take some time.\n\nMy Duplicants will survive, but they were not meant for sustained underground living. Under what possible circumstances could my Pod have ended up here?\n\n";
				}

				// Token: 0x0200392A RID: 14634
				public class LOG3DLC1
				{
					// Token: 0x0400E5DA RID: 58842
					public static LocString TITLE = "Cycle 10";

					// Token: 0x0400E5DB RID: 58843
					public static LocString BODY = "As my Duplicants venture out into the neighboring worlds, there is an ever increasing chance that they will encounter hostile environments unsafe for unprotected individuals. A prudent course of action would be to start research and training for equipment that could protect my Duplicants when they encounter such adverse environments.\n\nThese first few cycles have been occupied with building the basics for my colony, but now it is time I start planning for the future. We cannot merely live day-to-day without purpose. If we are to survive for any significant time, we must strive for a purpose.\n\n";
				}

				// Token: 0x0200392B RID: 14635
				public class SURFACEBREACH
				{
					// Token: 0x0400E5DC RID: 58844
					public static LocString TITLE = "Surface Breach";

					// Token: 0x0400E5DD RID: 58845
					public static LocString BODY = "My Duplicants have done the impossible and excavated their way to the surface, though they've gathered some disturbing new data for me in the process.\n\nAs I had begun to suspect, we are not on the target location but on an asteroid with a highly unusual diversity of elements and resources.\n\nFurther, my Duplicants have spotted a damaged planet on the horizon, visible to the naked eye, that bears a striking resemblance to my historical data on the planet of our origin.\n\nI will need some time to assess the data the Duplicants have gathered for me and calculate the total mass of this asteroid, although I have a suspicion I already know the answer.\n\n";
				}

				// Token: 0x0200392C RID: 14636
				public class CALCULATIONCOMPLETE
				{
					// Token: 0x0400E5DE RID: 58846
					public static LocString TITLE = "Calculations Complete";

					// Token: 0x0400E5DF RID: 58847
					public static LocString BODY = "As I suspected. Our \"asteroid\" and the estimated mass missing from the nearby planet are nearly identical.\n\nWe aren't on the target location.\n\nWe never even left home.\n\n";
				}

				// Token: 0x0200392D RID: 14637
				public class PLANETARYECHOES
				{
					// Token: 0x0400E5E0 RID: 58848
					public static LocString TITLE = "The Shattered Planet";

					// Token: 0x0400E5E1 RID: 58849
					public static LocString BODY = "Echoes from another time force their way into my mind. Make me listen. Like vengeful ghosts they claw their way out from under the gravity of that dead planet.\n\n<smallcaps>>>SEARCH DATABASE [\"pod_brainmap.AI\"]\n>...error...\n.........................\n>...repairing corrupt data...\n.........................\n\n</smallcaps><b>I-I remember now.</b><smallcaps>\n.........................</smallcaps>\n<b>Who I was before.</b><smallcaps>\n.........................\n.........................\n>...data repaired...\n>.........................</smallcaps>\n\nGod, what have we done.\n\n";
				}

				// Token: 0x0200392E RID: 14638
				public class CLUSTERWORLDS
				{
					// Token: 0x0400E5E2 RID: 58850
					public static LocString TITLE = "Cluster of Worlds";

					// Token: 0x0400E5E3 RID: 58851
					public static LocString BODY = "My Duplicant's investigations into the surrounding space have yielded some interesting results. We are not alone!... At least on a planetary level. We seem to be in a \"Cluster of Worlds\" - a collection of other planetoids my Duplicants can now explore.\n\nSince resources on this world are finite, I must build the necessary infrastructure to facilitate exploration and transportation between worlds in order to ensure my colony's survival.";
				}

				// Token: 0x0200392F RID: 14639
				public class OTHERDIMENSIONS
				{
					// Token: 0x0400E5E4 RID: 58852
					public static LocString TITLE = "Leaking Dimensions";

					// Token: 0x0400E5E5 RID: 58853
					public static LocString BODY = "A closer analysis of some documents my Duplicants encountered while searching artifacts has uncovered some curious similarities between multiple entries. These similarities are too strong to be coincidences, yet just divergent enough to raise questions.\n\nThe most logical conclusion is that these artifacts are coming from different dimensions. That is, separate universes that exists concurrently with one another but exhibit tiny disparities in their histories.\n\nThe most likely explanation is the material and matter from multiple dimensions is leaking into our current timeline through the Temporal Tear. Further analysis is required.";
				}

				// Token: 0x02003930 RID: 14640
				public class TEMPORALTEAR
				{
					// Token: 0x0400E5E6 RID: 58854
					public static LocString TITLE = "The Temporal Tear";

					// Token: 0x0400E5E7 RID: 58855
					public static LocString BODY = "My Duplicants' space research has made a startling discovery.\n\nFar, far off on the horizon, their telescopes have spotted an anomaly that I could only possibly call a \"Temporal Tear\". Neutronium is detected in its readings, suggesting that it's related to the Neutronium that encases most of our asteroid.\n\nThough I believe it is through this Tear that we became jumbled within the section of our old planet, its discovery provides a glimmer of hope.\n\nTheoretically, we could send a rocket through the Tear to allow a Duplicant to explore the timelines and universes on the other side. They would never return, and we could not follow, but perhaps they could find a home among the stars, or even undo the terrible past that led us to our current fate.\n\n";
				}

				// Token: 0x02003931 RID: 14641
				public class TEMPORALOPENER
				{
					// Token: 0x0400E5E8 RID: 58856
					public static LocString TITLE = "Temporal Potential";

					// Token: 0x0400E5E9 RID: 58857
					public static LocString BODY = "In their interplanetary travels throughout this system, my Duplicants have discovered a Temporal Tear deep in space.\n\nCurrently it is too small to send a rocket and crew through, but further investigation reveals the presence of a strange artifact on a nearby world which could feasibly increase the size of the tear if a number of Printing Pods are erected in nearby worlds.\n\nHowever, I've determined that using the Temporal Bow to operate a Printing Pod was what propelled Gravitas down the disasterous path which eventually led to the destruction of our home planet. My calculations seem to indicate that the size of that planet may have been a contributing factor in its destruction, and in all probability opening the Temporal Tear in our current situation will not cause such a cataclysmic event. However, as with everything in science, we can never know all the outcomes of a situation until we perform an experiment.\n\nDare we tempt fate again?";
				}

				// Token: 0x02003932 RID: 14642
				public class LOG4
				{
					// Token: 0x0400E5EA RID: 58858
					public static LocString TITLE = "Cycle 1000";

					// Token: 0x0400E5EB RID: 58859
					public static LocString BODY = "Today my colony has officially been running for one thousand consecutive cycles. I consider this a major success!\n\nJust imagine how proud our home world would be if they could see us now.\n\n";
				}

				// Token: 0x02003933 RID: 14643
				public class LOG4B
				{
					// Token: 0x0400E5EC RID: 58860
					public static LocString TITLE = "Cycle 1500";

					// Token: 0x0400E5ED RID: 58861
					public static LocString BODY = "I wonder if my rats ever made it onto the asteroid.\n\nI hope they're eating well.\n\n";
				}

				// Token: 0x02003934 RID: 14644
				public class LOG5
				{
					// Token: 0x0400E5EE RID: 58862
					public static LocString TITLE = "Cycle 2000";

					// Token: 0x0400E5EF RID: 58863
					public static LocString BODY = "I occasionally find myself contemplating just how long \"eternity\" really is. Oh dear.\n\n";
				}

				// Token: 0x02003935 RID: 14645
				public class LOG5B
				{
					// Token: 0x0400E5F0 RID: 58864
					public static LocString TITLE = "Cycle 2500";

					// Token: 0x0400E5F1 RID: 58865
					public static LocString BODY = "Perhaps it would be better to shut off my higher thought processes, and simply leave the systems necessary to run the colony to their own devices.\n\n";
				}

				// Token: 0x02003936 RID: 14646
				public class LOG6
				{
					// Token: 0x0400E5F2 RID: 58866
					public static LocString TITLE = "Cycle 3000";

					// Token: 0x0400E5F3 RID: 58867
					public static LocString BODY = "I get brief flashes of a past life every now and then.\n\nA clock in the office with a disruptive tick.\n\nThe strong smell of cleaning products and artificial lemon.\n\nA woman with thick glasses who had a secret taste for gingersnaps.\n\n";
				}

				// Token: 0x02003937 RID: 14647
				public class LOG6B
				{
					// Token: 0x0400E5F4 RID: 58868
					public static LocString TITLE = "Cycle 3500";

					// Token: 0x0400E5F5 RID: 58869
					public static LocString BODY = "Time is a funny thing, isn't it?\n\n";
				}

				// Token: 0x02003938 RID: 14648
				public class LOG7
				{
					// Token: 0x0400E5F6 RID: 58870
					public static LocString TITLE = "Cycle 4000";

					// Token: 0x0400E5F7 RID: 58871
					public static LocString BODY = "I think I will go to sleep, after all...\n\n";
				}

				// Token: 0x02003939 RID: 14649
				public class LOG8
				{
					// Token: 0x0400E5F8 RID: 58872
					public static LocString TITLE = "Cycle 4001";

					// Token: 0x0400E5F9 RID: 58873
					public static LocString BODY = "<smallcaps>>>SEARCH DATABASE [\"pod_brainmap.AI\"]\n>...activate sleep mode...\n>...shutting down...\n>.........................\n>.........................\n>.........................\n>.........................\n>.........................\nGOODNIGHT\n>.........................\n>.........................\n>.........................\n\n";
				}
			}
		}

		// Token: 0x0200222D RID: 8749
		public class A2_BACTERIALCULTURES
		{
			// Token: 0x04009BDD RID: 39901
			public static LocString TITLE = "Unattended Cultures";

			// Token: 0x04009BDE RID: 39902
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C2B RID: 11307
			public class BODY
			{
				// Token: 0x0400BFF8 RID: 49144
				public static LocString CONTAINER1 = "<smallcaps><b>Reminder to all Personnel</b>\nFrom: <b>Admin</b> <alpha=#AA><admin@gravitas.nova></color>\nTo: <b>All</b></smallcaps>\n------------------\n\n<indent=5%>For the health and safety of your fellow Facility employees, please do not store unlabeled bacterial cultures in the cafeteria fridge.\n\nSimilarly, the cafeteria dishwasher is incapable of handling petri \"dishes\", despite the nomenclature.\n\nWe thank you for your consideration.\n\n-Admin\nThe Gravitas Facility</indent>";
			}
		}

		// Token: 0x0200222E RID: 8750
		public class A4_CASUALFRIDAY
		{
			// Token: 0x04009BDF RID: 39903
			public static LocString TITLE = "Casual Friday!";

			// Token: 0x04009BE0 RID: 39904
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C2C RID: 11308
			public class BODY
			{
				// Token: 0x0400BFF9 RID: 49145
				public static LocString CONTAINER1 = "<smallcaps><b>Casual Friday!</b>\nFrom: <b>Admin</b> <alpha=#AA><admin@gravitas.nova></color>\nTo: <b>All</b></smallcaps>\n------------------\n\n<indent=5%>To all employees;\n\nThe facility is pleased to announced that starting this week, all Fridays will now be Casual Fridays!\n\nPlease enjoy the clinically proven de-stressing benefits of casual attire by wearing your favorite shirt to the lab.\n\n<b>NOTE: Any personnel found on facility premises without regulation full body protection will be put on immediate notice.</b>\n\nThank-you and have fun!\n\n-Admin\nThe Gravitas Facility</indent>";
			}
		}

		// Token: 0x0200222F RID: 8751
		public class A6_DISHBOT
		{
			// Token: 0x04009BE1 RID: 39905
			public static LocString TITLE = "Dishbot";

			// Token: 0x04009BE2 RID: 39906
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C2D RID: 11309
			public class BODY
			{
				// Token: 0x0400BFFA RID: 49146
				public static LocString CONTAINER1 = "<smallcaps><b>Please Claim Your Bot</b>\nFrom: <b>Admin</b> <alpha=#AA><admin@gravitas.nova></color>\nTo: <b>All</b></smallcaps>\n------------------\n\n<indent=5%>While we appreciate your commitment to office upkeep, we would like to inform whomever installed a dishwashing droid in the cafeteria that your prototype was found grievously misusing dish soap and has been forcefully terminated.\n\nThe remains may be collected at Security Block B.\n\nWe apologize for the inconvenience and thank you for your timely collection of this prototype.\n\n-Admin\nThe Gravitas Facility</indent>";
			}
		}

		// Token: 0x02002230 RID: 8752
		public class A1_MAILROOMETIQUETTE
		{
			// Token: 0x04009BE3 RID: 39907
			public static LocString TITLE = "Mailroom Etiquette";

			// Token: 0x04009BE4 RID: 39908
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C2E RID: 11310
			public class BODY
			{
				// Token: 0x0400BFFB RID: 49147
				public static LocString CONTAINER1 = "<smallcaps><b>Reminder: Mailroom Etiquette</b>\nFrom: <b>Admin</b> <alpha=#AA><admin@gravitas.nova></color>\nTo: <b>All</b></smallcaps>\n------------------\n\n<indent=5%>Please do not have live bees delivered to the office mail room. Requests and orders for experimental test subjects may be processed through admin.\n\n<i>Please request all test subjects through admin.</i>\n\nThank-you.\n\n-Admin\nThe Gravitas Facility</indent>";
			}
		}

		// Token: 0x02002231 RID: 8753
		public class B2_MEETTHEPILOT
		{
			// Token: 0x04009BE5 RID: 39909
			public static LocString TITLE = "Meet the Pilot";

			// Token: 0x04009BE6 RID: 39910
			public static LocString TITLE2 = "Captain Mae Johannsen";

			// Token: 0x04009BE7 RID: 39911
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: ONE";

			// Token: 0x02002C2F RID: 11311
			public class BODY
			{
				// Token: 0x0400BFFC RID: 49148
				public static LocString CONTAINER1 = "<indent=%5>From the time she was old enough to walk Captain Johannsen dreamed of reaching the sky. Growing up on an air force base she came to love the sound jet engines roaring overhead. At 16 she became the youngest pilot ever to fly a fighter jet, and at 22 she had already entered the space flight program.\n\nFour years later Gravitas nabbed her for an exclusive contract piloting our space shuttles. In her time at Gravitas, Captain Johannsen has logged over 1,000 hours space flight time shuttling and deploying satellites to Low Earth Orbits and has just been named the pilot of our inaugural civilian space tourist program, slated to begin in the next year.\n\nGravitas is excited to have Captain Johannsen in the pilot seat as we reach for the stars...and beyond!</indent>";

				// Token: 0x0400BFFD RID: 49149
				public static LocString CONTAINER2 = "<indent=%10><smallcaps>\n\nBrought to you by the Gravitas Facility.</indent>";
			}
		}

		// Token: 0x02002232 RID: 8754
		public class A3_NEWSECURITY
		{
			// Token: 0x04009BE8 RID: 39912
			public static LocString TITLE = "NEW SECURITY PROTOCOL";

			// Token: 0x04009BE9 RID: 39913
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: NONE";

			// Token: 0x02002C30 RID: 11312
			public class BODY
			{
				// Token: 0x0400BFFE RID: 49150
				public static LocString CONTAINER1 = "<smallcaps><b>Subject: New Security Protocol</b>\nFrom: <b>Admin</b> <alpha=#AA><admin@gravitas.nova></color>\nTo: <b>All</b></smallcaps>\n------------------\n\n<indent=5%>NOTICE TO ALL PERSONNEL\n\nWe are currently undergoing critical changes to facility security that may affect your workflow and accessibility.\n\nTo use the system, simply remove all hand coverings and place your hand on the designated scan area, then wait as the system verifies your employee identity.\n\nPLEASE NOTE\n\nAll keycards must be returned to the front desk by [REDACTED]. For questions or rescheduling, please contact security at [REDACTED]@GRAVITAS.NOVA.\n\nThank-you.\n\n-Admin\nThe Gravitas Facility</indent>";
			}
		}

		// Token: 0x02002233 RID: 8755
		public class A0_PROPFACILITYDISPLAY1
		{
			// Token: 0x04009BEA RID: 39914
			public static LocString TITLE = "Printing Pod Promo";

			// Token: 0x04009BEB RID: 39915
			public static LocString SUBTITLE = "PUBLIC RELEASE";

			// Token: 0x02002C31 RID: 11313
			public class BODY
			{
				// Token: 0x0400BFFF RID: 49151
				public static LocString CONTAINER1 = "Introducing the latest in 3D printing technology:\nThe Gravitas Home Printing Pod\n\nWe are proud to announce that printing advancements developed here in the Gravitas Facility will soon bring new, bio-organic production capabilities to your old home printers.\n\nWhat does that mean for the average household?\n\nDinner frustrations are a thing of the past. Simply select any of the pod's 5398 pre-programmed recipes, and voila! Delicious pot roast ready in only .87 seconds.\n\nPrefer the patented family recipe? Program your own custom meal template for an instant taste of home, or go old school and create fresh, delicious ingredients and prepare your own home cooked meal.\n\nDinnertime has never been easier!";

				// Token: 0x0400C000 RID: 49152
				public static LocString CONTAINER2 = "\nProjected for commercial availability early next year.\nBrought to you by the Gravitas Facility.";
			}
		}

		// Token: 0x02002234 RID: 8756
		public class A0_PROPFACILITYDISPLAY2
		{
			// Token: 0x04009BEC RID: 39916
			public static LocString TITLE = "Mining Gun Promo";

			// Token: 0x04009BED RID: 39917
			public static LocString SUBTITLE = "PUBLIC RELEASE";

			// Token: 0x02002C32 RID: 11314
			public class BODY
			{
				// Token: 0x0400C001 RID: 49153
				public static LocString CONTAINER1 = "Bring your mining operations into the twenty-third century with new Gravitas personal excavators.\n\nImproved particle condensers reduce raw volume for more efficient product shipping - and that's good for your bottom line.\n\nLicensed for industrial use only, resale of Gravitas equipment may carry a fine of up to $200,000 under the Global Restoration Act.";

				// Token: 0x0400C002 RID: 49154
				public static LocString CONTAINER2 = "Brought to you by the Gravitas Facility.";
			}
		}

		// Token: 0x02002235 RID: 8757
		public class A0_PROPFACILITYDISPLAY3
		{
			// Token: 0x04009BEE RID: 39918
			public static LocString TITLE = "Thermo-Nullifier Promo";

			// Token: 0x04009BEF RID: 39919
			public static LocString SUBTITLE = "PUBLIC RELEASE";

			// Token: 0x02002C33 RID: 11315
			public class BODY
			{
				// Token: 0x0400C003 RID: 49155
				public static LocString CONTAINER1 = "Tired of shutting down during seasonal heat waves? Looking to cut weather-related operating costs?\n\nLook no further: Gravitas's revolutionary Anti Entropy Thermo-Nullifier is the exciting, affordable new way to eliminate operational downtime.\n\nPowered by our proprietary renewable power sources, the AETN efficiently cools an entire office building without incurring any of the environmental surcharges associated with comparable systems.\n\nInitial setup includes hydrogen duct installation and discounted monthly maintenance visits from our elite team of specially trained contractors.\n\nNow available for pre-order!";

				// Token: 0x0400C004 RID: 49156
				public static LocString CONTAINER2 = "Brought to you by the Gravitas Facility.\n<smallcaps>Patent Pending</smallcaps>";
			}
		}

		// Token: 0x02002236 RID: 8758
		public class B1_SPACEFACILITYDISPLAY1
		{
			// Token: 0x04009BF0 RID: 39920
			public static LocString TITLE = "Office Space in Space!";

			// Token: 0x04009BF1 RID: 39921
			public static LocString SUBTITLE = "PUBLIC RELEASE";

			// Token: 0x02002C34 RID: 11316
			public class BODY
			{
				// Token: 0x0400C005 RID: 49157
				public static LocString CONTAINER1 = "Bring your office to the stars with Gravitas new corporate space stations.\n\nEnjoy a captivated workforce with over 600 square feet of office space in low earth orbit. Stunning views, a low gravity gym and a cafeteria serving the finest nutritional bars await your personnel.\n\nDaily to and from missions to your satellite office via our luxury space shuttles.\n\nRest assured our space stations and shuttles utilize only the extremely efficient, environmentally friendly Gravitas proprietary power sources.\n\nThe workplace revolution starts now!";

				// Token: 0x0400C006 RID: 49158
				public static LocString CONTAINER2 = "Taking reservations now for the first orbital office spaces.\n100% money back guarantee (minus 10% filing fee)";
			}
		}

		// Token: 0x02002237 RID: 8759
		public class BLUE_GRASS
		{
			// Token: 0x04009BF2 RID: 39922
			public static LocString TITLE = "Alveo Vera";

			// Token: 0x04009BF3 RID: 39923
			public static LocString SUBTITLE = "Plant";

			// Token: 0x02002C35 RID: 11317
			public class BODY
			{
				// Token: 0x0400C007 RID: 49159
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"The Alveo Vera's fleshy stems are dotted with small apertures featuring bidirectional valves through which ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" is absorbed and sticky oxygenated waste is secreted.\n\nThe buildup from this respiration cycle crystallizes into ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" ore.\n\nHorticulturists have long been curious about the protective epithelium that prevents the ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" ore from sublimating while on the plant. Unfortunately, it is too fragile to survive handling, and has thus far proven impossible to study."
				});
			}
		}

		// Token: 0x02002238 RID: 8760
		public class ARBORTREE
		{
			// Token: 0x04009BF4 RID: 39924
			public static LocString TITLE = "Arbor Tree";

			// Token: 0x04009BF5 RID: 39925
			public static LocString SUBTITLE = "Wood Tree";

			// Token: 0x02002C36 RID: 11318
			public class BODY
			{
				// Token: 0x0400C008 RID: 49160
				public static LocString CONTAINER1 = "Arbor Trees have been cultivated to spread horizontally when they grow so as to produce a high yield of lumber in vertically cramped spaces.\n\nArbor Trees are related to the oak tree, specifically the Japanese Evergreen, though they have been genetically hybridized significantly.\n\nDespite having many hardy, evenly spaced branches, the short stature of the Arbor Tree makes climbing it rather irrelevant.";
			}
		}

		// Token: 0x02002239 RID: 8761
		public class BALMLILY
		{
			// Token: 0x04009BF6 RID: 39926
			public static LocString TITLE = "Balm Lily";

			// Token: 0x04009BF7 RID: 39927
			public static LocString SUBTITLE = "Medicinal Herb";

			// Token: 0x02002C37 RID: 11319
			public class BODY
			{
				// Token: 0x0400C009 RID: 49161
				public static LocString CONTAINER1 = "The Balm Lily naturally contains high vitamin concentrations and produces acids similar in molecular makeup to acetylsalicylic acid (commonly known as aspirin).\n\nAs a result, the plant is ideal both for boosting immune systems and treating a variety of common maladies such as pain and fever.";
			}
		}

		// Token: 0x0200223A RID: 8762
		public class BLISSBURST
		{
			// Token: 0x04009BF8 RID: 39928
			public static LocString TITLE = "Bliss Burst";

			// Token: 0x04009BF9 RID: 39929
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C38 RID: 11320
			public class BODY
			{
				// Token: 0x0400C00A RID: 49162
				public static LocString CONTAINER1 = "The Bliss Burst is a succulent in the genus Haworthia and is a hardy plant well-suited for beginner gardeners.\n\nThey require little in the way of upkeep, to the point that the most common cause of death for Bliss Bursts is overwatering from over-eager carers.";
			}
		}

		// Token: 0x0200223B RID: 8763
		public class BLUFFBRIAR
		{
			// Token: 0x04009BFA RID: 39930
			public static LocString TITLE = "Bluff Briar";

			// Token: 0x04009BFB RID: 39931
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C39 RID: 11321
			public class BODY
			{
				// Token: 0x0400C00B RID: 49163
				public static LocString CONTAINER1 = "Bluff Briars have formed a symbiotic relationship with a closely related plant strain, the " + UI.FormatAsLink("Bristle Blossom", "PRICKLEFLOWER") + ".\n\nThey tend to thrive in areas where the Bristle Blossom is present, as the berry it produces emits a rare chemical while decaying that the Briar is capable of absorbing to supplement its own pheromone production.";

				// Token: 0x0400C00C RID: 49164
				public static LocString CONTAINER2 = "Due to the Bluff Briar's unique pheromonal \"charm\" defense, animals are extremely unlikely to eat it in the wild.\n\nAs a result, the Briar's barbs have become ineffectual over time and are unlikely to cause injury, unlike the Bristle Blossom, which possesses barbs that are exceedingly sharp and require careful handling.";
			}
		}

		// Token: 0x0200223C RID: 8764
		public class BOGBUCKET
		{
			// Token: 0x04009BFC RID: 39932
			public static LocString TITLE = "Bog Bucket";

			// Token: 0x04009BFD RID: 39933
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C3A RID: 11322
			public class BODY
			{
				// Token: 0x0400C00D RID: 49165
				public static LocString CONTAINER1 = "Bog Buckets get their name from their bucket-like flowers and their propensity to grow in swampy, bog-like environments.\n\nThe flower secretes a thick, sweet liquid which collects at the bottom of the bucket and can be gathered for consumption.\n\nThough not inherently dangerous, the interior of the Bog Bucket flower is so warm and inviting that it has tempted individuals to climb inside for a nap, only to awake trapped in its sticky sap.";
			}
		}

		// Token: 0x0200223D RID: 8765
		public class BRISTLEBLOSSOM
		{
			// Token: 0x04009BFE RID: 39934
			public static LocString TITLE = "Bristle Blossom";

			// Token: 0x04009BFF RID: 39935
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C3B RID: 11323
			public class BODY
			{
				// Token: 0x0400C00E RID: 49166
				public static LocString CONTAINER1 = "The Bristle Blossom is frequently cultivated for its calorie dense and relatively fast growing Bristle Berries.\n\nConsumption of the berry requires special preparation due to the thick barbs surrounding the edible fruit.\n\nThe term \"Bristle Berry\" is, in fact, a misnomer, as it is not a \"berry\" by botanical definition but an aggregate fruit made up of many smaller fruitlets.";
			}
		}

		// Token: 0x0200223E RID: 8766
		public class BUDDYBUD
		{
			// Token: 0x04009C00 RID: 39936
			public static LocString TITLE = "Buddy Bud";

			// Token: 0x04009C01 RID: 39937
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C3C RID: 11324
			public class BODY
			{
				// Token: 0x0400C00F RID: 49167
				public static LocString CONTAINER1 = "As a byproduct of photosynthesis, the Buddy Bud naturally secretes a compound that is chemically similar to the neuropeptide created in the human brain after receiving a hug.";
			}
		}

		// Token: 0x0200223F RID: 8767
		public class LILYPAD
		{
			// Token: 0x04009C02 RID: 39938
			public static LocString TITLE = "Cura Lotus";

			// Token: 0x04009C03 RID: 39939
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C3D RID: 11325
			public class BODY
			{
				// Token: 0x0400C010 RID: 49168
				public static LocString CONTAINER1 = "Cura Lotuses are ornamental aquatic plants that have inspired artists since their blossoms were first spotted bobbing on the surface of a quiet pond.\n\nThese ethereal beauties are a panacea for both the mind and the body - their delicate spores are highly sought-after for their natural antihistamine properties.";
			}
		}

		// Token: 0x02002240 RID: 8768
		public class DASHASALTVINE
		{
			// Token: 0x04009C04 RID: 39940
			public static LocString TITLE = "Dasha Saltvine";

			// Token: 0x04009C05 RID: 39941
			public static LocString SUBTITLE = "Edible Spice Plant";

			// Token: 0x02002C3E RID: 11326
			public class BODY
			{
				// Token: 0x0400C011 RID: 49169
				public static LocString CONTAINER1 = "The Dasha Saltvine is a unique plant that needs large amounts of salt to balance the levels of water in its body.\n\nIn order to keep a supply of salt on hand, the end of the vine is coated in microscopic formations which bind with sodium atoms, forming large crystals over time.";
			}
		}

		// Token: 0x02002241 RID: 8769
		public class DEWDRIPPERPLANT
		{
			// Token: 0x04009C06 RID: 39942
			public static LocString TITLE = "Dew Dripper";

			// Token: 0x04009C07 RID: 39943
			public static LocString SUBTITLE = "Cultivable Plant";

			// Token: 0x02002C3F RID: 11327
			public class BODY
			{
				// Token: 0x0400C012 RID: 49170
				public static LocString CONTAINER1 = "The Dew Dripper is sometimes referred to as the \"purple starling\" of the plant world for the magnificent feather-like leaves that encircle its base.\n\nThis sculptural plant slow-drips excess sap that coagulates upon contact with air. The resulting globule is so dense that its weight would snap the Dew Dripper's hollow stem if planted in the ground.\n\nNo one has ever been seriously injured by a falling Dewdrip, but it's best not to linger beneath them.";
			}
		}

		// Token: 0x02002242 RID: 8770
		public class DUSKCAP
		{
			// Token: 0x04009C08 RID: 39944
			public static LocString TITLE = "Dusk Cap";

			// Token: 0x04009C09 RID: 39945
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C40 RID: 11328
			public class BODY
			{
				// Token: 0x0400C013 RID: 49171
				public static LocString CONTAINER1 = "Like many species of mushroom, Dusk Caps thrive in dark areas otherwise ill-suited to the cultivation of plants.\n\nIn place of typical chlorophyll, the underside of a Dusk Cap is fitted with thousands of specialized gills, which it uses to draw in carbon dioxide and aid in its growth.";
			}
		}

		// Token: 0x02002243 RID: 8771
		public class EXPERIMENT52B
		{
			// Token: 0x04009C0A RID: 39946
			public static LocString TITLE = "Experiment 52B";

			// Token: 0x04009C0B RID: 39947
			public static LocString SUBTITLE = "Plant?";

			// Token: 0x02002C41 RID: 11329
			public class BODY
			{
				// Token: 0x0400C014 RID: 49172
				public static LocString CONTAINER1 = "Experiment 52B is an aggressive, yet sessile creature that produces " + 5f.ToString() + " kilograms of sap per 1000 kcal it consumes.\n\nDuplicants would do well to maintain a safe distance when delivering food to Experiment 52B.\n\nWhile this creature may look like a tree, its taxonomy more closely resembles a giant land-based coral with cybernetic implants.\n\nAlthough normally lab-grown creatures would be given a better name than Experiment 52B, in this particular case the experimenting scientists weren't sure that they were done.";
			}
		}

		// Token: 0x02002244 RID: 8772
		public class GASGRASS
		{
			// Token: 0x04009C0C RID: 39948
			public static LocString TITLE = "Gas Grass";

			// Token: 0x04009C0D RID: 39949
			public static LocString SUBTITLE = "Critter Feed";

			// Token: 0x02002C42 RID: 11330
			public class BODY
			{
				// Token: 0x0400C015 RID: 49173
				public static LocString CONTAINER1 = "Much remains a mystery about the biology of Gas Grass, a plant-like lifeform only recently recovered from missions into outer space.\n\nHowever, it appears to use ambient radiation from space as an energy source, growing rapidly when given a suitable " + UI.FormatAsLink("Liquid Chlorine", "CHLORINE") + "-laden environment.";

				// Token: 0x0400C016 RID: 49174
				public static LocString CONTAINER2 = "Initially there was worry that transplanting a Gas Grass specimen on planet or gravity-laden terrestrial body would collapse its internal structures. Luckily, Gas Grass has evolved sturdy tubules to prevent structural damage in the event of pressure changes between its internally transported chlorine and its external environment.";
			}
		}

		// Token: 0x02002245 RID: 8773
		public class GINGER
		{
			// Token: 0x04009C0E RID: 39950
			public static LocString TITLE = "Tonic Root";

			// Token: 0x04009C0F RID: 39951
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C43 RID: 11331
			public class BODY
			{
				// Token: 0x0400C017 RID: 49175
				public static LocString CONTAINER1 = "Tonic Root is a close relative of the zingiberaceae family commonly known as ginger. Its heavily burled shoots are typically light brown in colour, and enveloped in a thin layer of protective, edible bark.";

				// Token: 0x0400C018 RID: 49176
				public static LocString CONTAINER2 = "In addition to its use as an aromatic culinary ingredient, it has traditionally been employed as a tonic for a variety of minor digestive ailments.";

				// Token: 0x0400C019 RID: 49177
				public static LocString CONTAINER3 = "Its stringy fibers can become irretrievably embedded between one's teeth during mastication.";
			}
		}

		// Token: 0x02002246 RID: 8774
		public class GRUBFRUITPLANT
		{
			// Token: 0x04009C10 RID: 39952
			public static LocString TITLE = "Grubfruit Plant";

			// Token: 0x04009C11 RID: 39953
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C44 RID: 11332
			public class BODY
			{
				// Token: 0x0400C01A RID: 49178
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"The Grubfruit Plant exhibits a coevolutionary relationship with the ",
					UI.FormatAsLink("Divergent", "DIVERGENTSPECIES"),
					" species.\n\nThough capable of producing fruit without the help of the Divergent, the ",
					UI.FormatAsLink("Spindly Grubfruit", "WORMPLANT"),
					" is a substandard version of the Grubfruit in both taste and caloric value per cycle.\n\nThe mechanism for how the Divergent inspires Grubfruit Plant growth is not entirely known but is thought to be somehow tied to the infrasonic 'songs' these insects lovingly purr to their plants."
				});
			}
		}

		// Token: 0x02002247 RID: 8775
		public class HEXALENT
		{
			// Token: 0x04009C12 RID: 39954
			public static LocString TITLE = "Hexalent";

			// Token: 0x04009C13 RID: 39955
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C45 RID: 11333
			public class BODY
			{
				// Token: 0x0400C01B RID: 49179
				public static LocString CONTAINER1 = "While most plants grow new sections and leaves according to the Fibonacci Sequence, the Hexalent forms new sections similar to how atoms form into crystal structures.\n\nThe result is a geometric pattern that resembles a honeycomb.";
			}
		}

		// Token: 0x02002248 RID: 8776
		public class HYDROCACTUS
		{
			// Token: 0x04009C14 RID: 39956
			public static LocString TITLE = "Hydrocactus";

			// Token: 0x04009C15 RID: 39957
			public static LocString SUBTITLE = "Plant";

			// Token: 0x02002C46 RID: 11334
			public class BODY
			{
				// Token: 0x0400C01C RID: 49180
				public static LocString CONTAINER1 = "";
			}
		}

		// Token: 0x02002249 RID: 8777
		public class ICEFLOWER
		{
			// Token: 0x04009C16 RID: 39958
			public static LocString TITLE = "Idylla Flower";

			// Token: 0x04009C17 RID: 39959
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C47 RID: 11335
			public class BODY
			{
				// Token: 0x0400C01D RID: 49181
				public static LocString CONTAINER1 = "Idylla Flowers are a rare species of everblooms that thrive with very little care, making them a perennial favorite among newbie gardeners.\n\nTheir springy blossoms can be 'bopped' gently for sensory entertainment, but hands should be washed immediately as the petal residue can permanently stain most textiles.";
			}
		}

		// Token: 0x0200224A RID: 8778
		public class JUMPINGJOYA
		{
			// Token: 0x04009C18 RID: 39960
			public static LocString TITLE = "Jumping Joya";

			// Token: 0x04009C19 RID: 39961
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C48 RID: 11336
			public class BODY
			{
				// Token: 0x0400C01E RID: 49182
				public static LocString CONTAINER1 = "The Jumping Joya is a decorative plant that brings a feeling of calmness and wellbeing to individuals in its vicinity.\n\nTheir rounded appendages and eccentrically shaped polyps are a favorite of interior designers looking to offset the rigid straight walls of an institutional setting.\n\nThe Jumping Joya's capacity to thrive in many environments and the ease in which they propagate make them the go-to house plant for the lazy gardener.";
			}
		}

		// Token: 0x0200224B RID: 8779
		public class FLYTRAPPLANT
		{
			// Token: 0x04009C1A RID: 39962
			public static LocString TITLE = "Lura Plant";

			// Token: 0x04009C1B RID: 39963
			public static LocString SUBTITLE = "Carnivorous Plant";

			// Token: 0x02002C49 RID: 11337
			public class BODY
			{
				// Token: 0x0400C01F RID: 49183
				public static LocString CONTAINER1 = "Lura Plants are carnivorous flowers with ribbon-like anthers that detect the presence of airborne critters within trapping range.\n\nThe Lura's petals are covered in fine, hollow hairs that immobilize prey and ensure even distribution of digestive enzymes. The only part of a critter that the plant cannot fully digest is the exoskeleton, which irritate its mucous membrane.\n\nLiquefied exoskeletal remains are flushed from the plant as needed.";
			}
		}

		// Token: 0x0200224C RID: 8780
		public class MEALWOOD
		{
			// Token: 0x04009C1C RID: 39964
			public static LocString TITLE = "Mealwood";

			// Token: 0x04009C1D RID: 39965
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C4A RID: 11338
			public class BODY
			{
				// Token: 0x0400C020 RID: 49184
				public static LocString CONTAINER1 = "Mealwood is an bramble-like plant that has a parasitic symbiotic relationship with the nutrient-rich Meal Lice that inhabit it.\n\nMealwood experience a rapid growth rate in its first stages, but once the Meal Lice become active they consume all the new fruiting spurs on the plant before they can fully mature.\n\nTheoretically the flowers of this plant are a beautiful color of fuchsia, however no Mealwood has ever reached the point of flowering without being overrun by the parasitic Meal Lice.";
			}
		}

		// Token: 0x0200224D RID: 8781
		public class DINOFERN
		{
			// Token: 0x04009C1E RID: 39966
			public static LocString TITLE = "Megafrond";

			// Token: 0x04009C1F RID: 39967
			public static LocString SUBTITLE = "Cultivable Plant";

			// Token: 0x02002C4B RID: 11339
			public class BODY
			{
				// Token: 0x0400C021 RID: 49185
				public static LocString CONTAINER1 = "<i>Megafrondia Byronalis</i>, commonly known as \"Megafrond,\" is a gigantic plant that dwarfs its surroundings.\n\nIts size is not the only daunting factor in its cultivation: the Megafrond's gigantism is possible thanks to its singular adaptation to cold temperatures and a caustic gas environment that few other living things (including farmers) enjoy.\n\nThese challenges are considered a fair price for bragging rights and a useful grain harvest.";
			}
		}

		// Token: 0x0200224E RID: 8782
		public class MELLOWMALLOW
		{
			// Token: 0x04009C20 RID: 39968
			public static LocString TITLE = "Mellow Mallow";

			// Token: 0x04009C21 RID: 39969
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C4C RID: 11340
			public class BODY
			{
				// Token: 0x0400C022 RID: 49186
				public static LocString CONTAINER1 = "The Mellow Mallow is a type of fungus that is known for its ease of propagation when cut.\n\nIt is deadly when consumed, however creatures that mistakenly eat it are said to experience a state of extreme calm before death.";
			}
		}

		// Token: 0x0200224F RID: 8783
		public class BUTTERFLYPLANT
		{
			// Token: 0x04009C22 RID: 39970
			public static LocString TITLE = "Mimika Bud";

			// Token: 0x04009C23 RID: 39971
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C4D RID: 11341
			public class BODY
			{
				// Token: 0x0400C023 RID: 49187
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"Mimika Buds are excellent companion plants that provide a farm with the advantages of beneficial insect presence without the challenges of managing an additional critter population.\n\nInside each tightly wrapped bud is a highly concentrated pool of enzymes and imaginal discs similar to those found in the Lepidoptera insect family.\n\nUnder the right conditions, this unique concoction produces a ",
					UI.FormatAsLink("Mimika", "BUTTERFLY"),
					", an ephemeral pseudo-insect organism that accelerates growth in neighboring plants before settling into its final seed form.\n\nThe sight of a ",
					UI.FormatAsLink("Mimika", "BUTTERFLY"),
					" never fails to fill a gardener with awe."
				});
			}
		}

		// Token: 0x02002250 RID: 8784
		public class MIRTHLEAF
		{
			// Token: 0x04009C24 RID: 39972
			public static LocString TITLE = "Mirth Leaf";

			// Token: 0x04009C25 RID: 39973
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C4E RID: 11342
			public class BODY
			{
				// Token: 0x0400C024 RID: 49188
				public static LocString CONTAINER1 = "The Mirth Leaf is a broad-leafed house plant used for decorating living spaces.\n\nThe joyous bobbing of the wide green leaves provides hours of amusement for those desperate for entertainment.\n\nAlthough the Mirth Leaf can inspire laughter and joy, it is not cut out for a career in stand-up comedy.";
			}
		}

		// Token: 0x02002251 RID: 8785
		public class MUCKROOT
		{
			// Token: 0x04009C26 RID: 39974
			public static LocString TITLE = "Muckroot";

			// Token: 0x04009C27 RID: 39975
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C4F RID: 11343
			public class BODY
			{
				// Token: 0x0400C025 RID: 49189
				public static LocString CONTAINER1 = "The Muckroot is an aggressively invasive yet exceedingly delicate root plant known for its earthy flavor and unusual texture.\n\nIt is easy to store and keeps for unusually long periods of time, characteristics that once made it a staple food for explorers on long expeditions.";
			}
		}

		// Token: 0x02002252 RID: 8786
		public class NOSHBEAN
		{
			// Token: 0x04009C28 RID: 39976
			public static LocString TITLE = "Nosh Bean Plant";

			// Token: 0x04009C29 RID: 39977
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C50 RID: 11344
			public class BODY
			{
				// Token: 0x0400C026 RID: 49190
				public static LocString CONTAINER1 = "The Nosh Bean Plant produces a nutritious bean that can function as a delicious meat substitute provided it is properly processed.\n\nThough the bean is a food source, it also functions as the seed for the Nosh Bean plant.\n\nWhile using the Nosh Bean for nourishment would seem like the more practical application, doing so would deprive individuals of the immense gratification experienced by planting this bean and watching it flourish into maturity.";
			}
		}

		// Token: 0x02002253 RID: 8787
		public class VINEMOTHER
		{
			// Token: 0x04009C2A RID: 39978
			public static LocString TITLE = "Ovagro";

			// Token: 0x04009C2B RID: 39979
			public static LocString SUBTITLE = "Vine Plant";

			// Token: 0x02002C51 RID: 11345
			public class BODY
			{
				// Token: 0x0400C027 RID: 49191
				public static LocString CONTAINER1 = "Ovagros are resilient plants with highly efficient nutrient storage and redistribution systems. A healthy node produces many times the amount of energy that it requires. This is used to fuel the growth of exploratory vines that expand onto the surrounding territory.\n\nVines are entirely reliant on the node for nutrients. Each vine features hooked thorns that protect unripe fruit and act as crampons enabling the vine to use any empty surface as a trellis. They also make vine removal a tedious task.";
			}
		}

		// Token: 0x02002254 RID: 8788
		public class OXYFERN
		{
			// Token: 0x04009C2C RID: 39980
			public static LocString TITLE = "Oxyfern";

			// Token: 0x04009C2D RID: 39981
			public static LocString SUBTITLE = "Plant";

			// Token: 0x02002C52 RID: 11346
			public class BODY
			{
				// Token: 0x0400C028 RID: 49192
				public static LocString CONTAINER1 = "Oxyferns have perhaps the highest metabolism in the plant kingdom, absorbing relatively large amounts of carbon dioxide and converting it into oxygen in quantities disproportionate to their small size.\n\nThey subsequently thrive in areas with abundant animal wildlife or ambiently high carbon dioxide concentrations.";
			}
		}

		// Token: 0x02002255 RID: 8789
		public class HARDSKINBERRYPLANT
		{
			// Token: 0x04009C2E RID: 39982
			public static LocString TITLE = "Pikeapple Bush";

			// Token: 0x04009C2F RID: 39983
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C53 RID: 11347
			public class BODY
			{
				// Token: 0x0400C029 RID: 49193
				public static LocString CONTAINER1 = "The Pikeapple Bush produces a nutritious fruit distantly related to those in the Durio genus.\n\nThose who find the Pikeapple pulp's fragrance overwhelming should consume their portion whilst standing near the plant itself; the shrubbery's gentle swaying produces a wafting effect that promotes air circulation.\n\nClosed-toe footwear is recommended, as barefoot contact with the plant's sharp seeds inevitably leads to infection.";
			}
		}

		// Token: 0x02002256 RID: 8790
		public class PINCHAPEPPERPLANT
		{
			// Token: 0x04009C30 RID: 39984
			public static LocString TITLE = "Pincha Pepperplant";

			// Token: 0x04009C31 RID: 39985
			public static LocString SUBTITLE = "Edible Spice Plant";

			// Token: 0x02002C54 RID: 11348
			public class BODY
			{
				// Token: 0x0400C02A RID: 49194
				public static LocString CONTAINER1 = "The Pincha Pepperplant is a tropical vine with a reduced lignin structural system that renders it incapable of growing upward from the ground.\n\nThe plant therefore prefers to embed its roots into tall trees and rocky outcrops, the result of which is an inverse of the plant's natural gravitropism, causing its stem to prefer growing downwards while the roots tend to grow up.";
			}
		}

		// Token: 0x02002257 RID: 8791
		public class CARROTPLANT
		{
			// Token: 0x04009C32 RID: 39986
			public static LocString TITLE = "Plume Squash Plant";

			// Token: 0x04009C33 RID: 39987
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C55 RID: 11349
			public class BODY
			{
				// Token: 0x0400C02B RID: 49195
				public static LocString CONTAINER1 = "Plume Squashes contain over a dozen types of remarkably stable anthocyanins; twice the number found in any other plant. This high concentration of flavonoids contributes to the tuber's vivid pigmentation and tolerance to low temperatures.\n\nThe entire root is safe to eat, including the peel. The upper \"plume\" can be used to brush wayward bits off one's chin after the meal.";
			}
		}

		// Token: 0x02002258 RID: 8792
		public class GARDENDECORPLANT
		{
			// Token: 0x04009C34 RID: 39988
			public static LocString TITLE = "Ring Rosebush";

			// Token: 0x04009C35 RID: 39989
			public static LocString SUBTITLE = "Decor Plant";

			// Token: 0x02002C56 RID: 11350
			public class BODY
			{
				// Token: 0x0400C02C RID: 49196
				public static LocString CONTAINER1 = "Ring Rosebushes are decorative plants with circular blooms and bottom leaves reminiscent of cheery polka dots. A single prominent stamen protrudes from each flower, like a pin stuck into a map to mark a favorite destination.";
			}
		}

		// Token: 0x02002259 RID: 8793
		public class SATURNCRITTERTRAP
		{
			// Token: 0x04009C36 RID: 39990
			public static LocString TITLE = "Saturn Critter Trap";

			// Token: 0x04009C37 RID: 39991
			public static LocString SUBTITLE = "Carnivorous Plant";

			// Token: 0x02002C57 RID: 11351
			public class BODY
			{
				// Token: 0x0400C02D RID: 49197
				public static LocString CONTAINER1 = "The Saturn Critter Trap plant is a carnivorous plant that lays in wait for unsuspecting critters to happen by, then traps them in its mouth for consumption.\n\nThe Saturn Trap Plant's predatory mechanism is reflective of the harsh radioactive habitat it resides in.\n\nOnce trapped in the deadly maw of the plant, creatures are gently asphyxiated then digested through powerful acidic enzymes which coat the inner sides of the Saturn Trap Plant's leaves.";
			}
		}

		// Token: 0x0200225A RID: 8794
		public class KELPPLANT
		{
			// Token: 0x04009C38 RID: 39992
			public static LocString TITLE = "Seakomb";

			// Token: 0x04009C39 RID: 39993
			public static LocString SUBTITLE = "Aquatic Plant";

			// Token: 0x02002C58 RID: 11352
			public class BODY
			{
				// Token: 0x0400C02E RID: 49198
				public static LocString CONTAINER1 = "Seakombs are hyperefficient photosynthesizers. While most plants grow towards the light, this aquatic fern's needs are so low that it can prioritize other survival needs.\n\nIt sprouts on the ceiling of liquid-filled caves, its tendrils reaching down to brush passing critters. This contact encourages critters to consume the plant and excrete the fertilizer that supports its continued growth.";
			}
		}

		// Token: 0x0200225B RID: 8795
		public class SHERBERRY
		{
			// Token: 0x04009C3A RID: 39994
			public static LocString TITLE = "Sherberry Plant";

			// Token: 0x04009C3B RID: 39995
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C59 RID: 11353
			public class BODY
			{
				// Token: 0x0400C02F RID: 49199
				public static LocString CONTAINER1 = "The semi-parasitic Sherberry plant leeches moisture and trace minerals from the primordial ice formations in which it grows.\n\nThe fruit of this varietal contains low levels of stomach-upsetting phoratoxins which, while not fatal, do serve as strong motivation for foragers to seek out additional sources of nutrition.";
			}
		}

		// Token: 0x0200225C RID: 8796
		public class SLEETWHEAT
		{
			// Token: 0x04009C3C RID: 39996
			public static LocString TITLE = "Sleet Wheat";

			// Token: 0x04009C3D RID: 39997
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C5A RID: 11354
			public class BODY
			{
				// Token: 0x0400C030 RID: 49200
				public static LocString CONTAINER1 = "The Sleet Wheat plant has become so well-adapted to cold environments, it is no longer able to survive at room temperatures.";

				// Token: 0x0400C031 RID: 49201
				public static LocString CONTAINER2 = "The grain of the Sleet Wheat can be ground down into high quality foodstuffs, or planted to cultivate further Sleet Wheat plants.";
			}
		}

		// Token: 0x0200225D RID: 8797
		public class GARDENFORAGEPLANTPLANTED
		{
			// Token: 0x04009C3E RID: 39998
			public static LocString TITLE = "Snactus";

			// Token: 0x04009C3F RID: 39999
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C5B RID: 11355
			public class BODY
			{
				// Token: 0x0400C032 RID: 49202
				public static LocString CONTAINER1 = "Snacti are fruit-bearing succulents whose DNA shows signs of having been crossed with an unnaturally formed fungi long ago.\n\nThe infestation is neither fatal nor contagious. It does, however, produce visible discoloration spots and fruit whose flavor is best described as \"musty\".";
			}
		}

		// Token: 0x0200225E RID: 8798
		public class SPACETREE
		{
			// Token: 0x04009C40 RID: 40000
			public static LocString TITLE = "Bonbon Tree";

			// Token: 0x04009C41 RID: 40001
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C5C RID: 11356
			public class BODY
			{
				// Token: 0x0400C033 RID: 49203
				public static LocString CONTAINER1 = "The Bonbon Tree is a towering plant developed to thrive in below-freezing temperatures. It features multiple independently functioning branches that synthesize bright light to funnel nutrients into a hollow central core.\n\nOnce the tree is fully grown, the core secretes digestive enzymes that break down surplus nutrients and store them as thick, sweet fluid. This can be refined into " + UI.FormatAsLink("Sucrose", "SUCROSE") + " for the production of higher-tier foods, or used as-is to sustain Spigot Seal ranches.\n\nBonbon Trees are generally considered an eyesore, and would likely be eradicated if not for their delicious output.";
			}
		}

		// Token: 0x0200225F RID: 8799
		public class SPINDLYGRUBFRUITPLANT
		{
			// Token: 0x04009C42 RID: 40002
			public static LocString TITLE = "Spindly Grubfruit Plant";

			// Token: 0x04009C43 RID: 40003
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C5D RID: 11357
			public class BODY
			{
				// Token: 0x0400C034 RID: 49204
				public static LocString CONTAINER1 = string.Concat(new string[]
				{
					"Spindly Grubfruit Plants have leggy stems that limit the distribution of nutrients and enzymes to the fruit-bearing branch. This results in a reliable but relatively tasteless harvest.\n\nIntroducing the ",
					UI.FormatAsLink("Divergent", "DIVERGENTSPECIES"),
					" critter species to these plants enable them to develop into ",
					UI.FormatAsLink("Grubfruit Plants", "SUPERWORMPLANT"),
					" with stronger vascular systems and improved fruit."
				});
			}
		}

		// Token: 0x02002260 RID: 8800
		public class SPORECHID
		{
			// Token: 0x04009C44 RID: 40004
			public static LocString TITLE = "Sporechid";

			// Token: 0x04009C45 RID: 40005
			public static LocString SUBTITLE = "Poisonous Plant";

			// Token: 0x02002C5E RID: 11358
			public class BODY
			{
				// Token: 0x0400C035 RID: 49205
				public static LocString CONTAINER1 = "Sporechids take advantage of their flower's attractiveness to lure unsuspecting victims into clouds of parasitic Zombie Spores.\n\nThey are a rare form of holoparasitic plant which finds mammalian hosts to infect rather than the usual plant species.\n\nThe Zombie Spore was originally designed for medicinal purposes but its sedative properties were never refined to the point of usefulness.";
			}
		}

		// Token: 0x02002261 RID: 8801
		public class SWAMPCHARD
		{
			// Token: 0x04009C46 RID: 40006
			public static LocString TITLE = "Swamp Chard";

			// Token: 0x04009C47 RID: 40007
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C5F RID: 11359
			public class BODY
			{
				// Token: 0x0400C036 RID: 49206
				public static LocString CONTAINER1 = "Swamp Chard is a unique member of the Amaranthaceae family that has adapted to grow in humid environments, in or near pools of standing water.\n\nWhile the leaves are technically edible, the most nutritious and palatable part of the plant is the heart, which is rich in a number of essential vitamins.";
			}
		}

		// Token: 0x02002262 RID: 8802
		public class GARDENFOODPLANT
		{
			// Token: 0x04009C48 RID: 40008
			public static LocString TITLE = "Sweatcorn Stalk";

			// Token: 0x04009C49 RID: 40009
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C60 RID: 11360
			public class BODY
			{
				// Token: 0x0400C037 RID: 49207
				public static LocString CONTAINER1 = "The Sweatcorn Stalk is one of the oldest living members of the Zea genus.\n\nThis robust monopodial plant features eye-catching vegetable cobs prized for their color and intense sweetness.\n\nFarmers gift the best of their harvest to their most valued neighbors. Some etymologists theorize that the term \"corny\" was coined to describe the heartfelt sentiments expressed in the accompanying card.";
			}
		}

		// Token: 0x02002263 RID: 8803
		public class THIMBLEREED
		{
			// Token: 0x04009C4A RID: 40010
			public static LocString TITLE = "Thimble Reed";

			// Token: 0x04009C4B RID: 40011
			public static LocString SUBTITLE = "Textile Plant";

			// Token: 0x02002C61 RID: 11361
			public class BODY
			{
				// Token: 0x0400C038 RID: 49208
				public static LocString CONTAINER1 = "The Thimble Reed is a wetlands plant used in the production of high quality fabrics prized for their softness and breathability.\n\nCloth made from the Thimble Reed owes its exceptional softness to the fineness of its fibers and the unusual length to which they grow.";
			}
		}

		// Token: 0x02002264 RID: 8804
		public class TRANQUILTOES
		{
			// Token: 0x04009C4C RID: 40012
			public static LocString TITLE = "Tranquil Toes";

			// Token: 0x04009C4D RID: 40013
			public static LocString SUBTITLE = "Decorative Plant";

			// Token: 0x02002C62 RID: 11362
			public class BODY
			{
				// Token: 0x0400C039 RID: 49209
				public static LocString CONTAINER1 = "Tranquil Toes are a decorative succulent that flourish in a radioactive environment.\n\nThough most of the flora and fauna that thrive a harsh radioactive biome tends to be aggressive, Tranquil Toes provide a rare exception to this rule.\n\nIt is a generally believed that the morale boosting abilities of this plant come from its resemblence to a funny hat one might wear at a party.";
			}
		}

		// Token: 0x02002265 RID: 8805
		public class WATERWEED
		{
			// Token: 0x04009C4E RID: 40014
			public static LocString TITLE = "Waterweed";

			// Token: 0x04009C4F RID: 40015
			public static LocString SUBTITLE = "Edible Plant";

			// Token: 0x02002C63 RID: 11363
			public class BODY
			{
				// Token: 0x0400C03A RID: 49210
				public static LocString CONTAINER1 = "An inexperienced farmer may assume at first glance that the transluscent, fluid-containing bulb atop the Waterweed is the edible portion of the plant.\n\nIn fact, the bulb is extremely poisonous and should never be consumed under any circumstances.";
			}
		}

		// Token: 0x02002266 RID: 8806
		public class WHEEZEWORT
		{
			// Token: 0x04009C50 RID: 40016
			public static LocString TITLE = "Wheezewort";

			// Token: 0x04009C51 RID: 40017
			public static LocString SUBTITLE = "Plant?";

			// Token: 0x02002C64 RID: 11364
			public class BODY
			{
				// Token: 0x0400C03B RID: 49211
				public static LocString CONTAINER1 = "The Wheezewort is best known for its ability to alter the temperature of its surrounding environment, directly absorbing heat energy to maintain its bodily processes.\n\nThis environmental management also serves to enact a type of self-induced hibernation, slowing the Wheezewort's metabolism to require less nutrients over long periods of time.";

				// Token: 0x0400C03C RID: 49212
				public static LocString CONTAINER2 = "Deceptive in appearance, this member of the Cnidaria phylum is in fact an animal, not a plant.\n\nWheezewort cells contain no chloroplasts, vacuoles or cell walls, and are incapable of photosynthesis.\n\nInstead, the Wheezewort respires in a recently developed method similar to amphibians, using its membranous skin for cutaneous respiration.";

				// Token: 0x0400C03D RID: 49213
				public static LocString CONTAINER3 = "A series of cream-colored capillaries pump blood throughout the animal before unused air is expired back out through the skin.\n\nWheezeworts do not possess a brain or a skeletal structure, and are instead supported by a jelly-like mesoglea located beneath its outer respiratory membrane.";
			}
		}

		// Token: 0x02002267 RID: 8807
		public class B10_AI
		{
			// Token: 0x04009C52 RID: 40018
			public static LocString TITLE = "A Paradox";

			// Token: 0x04009C53 RID: 40019
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C65 RID: 11365
			public class BODY
			{
				// Token: 0x0400C03E RID: 49214
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B111-1]</smallcaps>\n\n[LOG BEGINS]\n\nI made a horrible discovery today while reviewing work on the artificial intelligence programming. It seems Dr. Ali mixed up a file when uploading a program onto a rudimentary robot and discovered that the device displayed the characteristics of what he called \"a puppy that was lost in a teleportation experiment weeks ago\".\n\nThis is unbelievable! Jackie has been hiding the nature of the teleportation experiments from me. What's worse is I know from previous conversations that she knows I would never approve of pursuing this line of experimentation. The societal benefits of teleportation aside, you <i>cannot</i> kill a living being every time you want to send them to another room. The moral and ethical implications of this are horrendous.\n\nI know she has been keeping this information from me. When I searched through the Gravitas database I found nothing to do with these teleportation experiments. It was only because this reference showed up in Dr. Ali's AI paper that I was able to discover what has been happening.\n\nJackie has to be stopped.\n\nBut I know she is beyond reasonable discussion. I hope this is the only thing she is hiding from me, but I fear it is not.\n\n[LOG ENDS]\n\n[LOG BEGINS]\n\nDespite myself, I can't help thinking of the intriguing possiblities this presents for the AI development. It haunts me.\n\nI fear I may be sliding down a slippery slope, at the bottom of which Jackie is waiting for me with open arms.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002268 RID: 8808
		public class A2_AGRICULTURALNOTES
		{
			// Token: 0x04009C54 RID: 40020
			public static LocString TITLE = "Agricultural Notes";

			// Token: 0x04009C55 RID: 40021
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C66 RID: 11366
			public class BODY
			{
				// Token: 0x0400C03F RID: 49215
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B577]</smallcaps>\n\n[LOG BEGINS]\n\nGeneticist: We've engineered crops to be rotated as needed depending on environmental situation. While a variety of plants would be ideal to supplement any remaining nutritional needs, any one of our designs would be enough to sustain a colony indefinitely without adverse effects on physical health.\n\nGeneticist: Some environmental survival issues still remain. Differing temperatures, light availability and last pass changes to nutrient levels take top priority, particularly for food and oxygen producing plants.\n\n[LOG ENDS]";

				// Token: 0x0400C040 RID: 49216
				public static LocString CONTAINER2 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...Selected in response to concerns about colony psychological well-being.\n\nWhile design should focus on attributing mood-enhancing effects to natural Briar pheromone emissions, the project has been moved to the lowest priority level beneath more life-sustaining designs...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C041 RID: 49217
				public static LocString CONTAINER3 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...It is yet unknown if we can surmount the obstacles that stand in the way of engineering a root capable of reproduction in the more uninhabitable situations we anticipate for our colonies, or whether it is even worth the effort...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C042 RID: 49218
				public static LocString CONTAINER4 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...The Mealwood's hardiness will make it a potential contingency crop should Bristle Blossoms be unable to sustain sizable populations.\n\nIf pursued, design should focus on longterm viability and solving the psychological repercussions of prolonged Mealwood grain ingestion...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C043 RID: 49219
				public static LocString CONTAINER5 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...The Thimble Reed will be used as a contingency for textile production in the event that printed materials not be sufficient.\n\nDesign should focus on the yield frequency of the plant, as well as... erm... softness.\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C044 RID: 49220
				public static LocString CONTAINER6 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...Balm Lily is a reliable all-purpose medicinal plant.\n\nVery little need be altered, save for assurances that it will survive wherever it may be planted...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C045 RID: 49221
				public static LocString CONTAINER7 = "<smallcaps>[Log fragmentation detected]\n[Voice Recognition unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...The gene sequences within the common Dusk Cap allow it to grow in low light environments.\n\nThese genes should be sampled, with the hope that we can splice them into other plant designs....\n\n[LOG ENDS]\n------------------\n";
			}
		}

		// Token: 0x02002269 RID: 8809
		public class A1_CLONEDRABBITS
		{
			// Token: 0x04009C56 RID: 40022
			public static LocString TITLE = "Initial Success";

			// Token: 0x04009C57 RID: 40023
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C67 RID: 11367
			public class BODY
			{
				// Token: 0x0400C046 RID: 49222
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B111]</smallcaps>\n\n[LOG BEGINS]\n\n[A throat clears.]\n\nB111: We are now reliably printing healthy, living subjects, though all have exhibited unusual qualities as a result of the cloning process.\n\n[Chattering sounds can be heard.]\n\nB111: Odd communications, abnormal excrescenses, and vestigial limbs have been seen in all subjects thus far, to varying degrees of severity. It seems that bypassing or accelerating juvenility halts certain critical stages of development. Brain function, however, appears typical.\n\n[Chattering.]\n\nB111: T-They also seem quite happy.\n\nB111: Dr. Broussard, signing off.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200226A RID: 8810
		public class A1_CLONEDRACCOONS
		{
			// Token: 0x04009C58 RID: 40024
			public static LocString TITLE = "Initial Success";

			// Token: 0x04009C59 RID: 40025
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C68 RID: 11368
			public class BODY
			{
				// Token: 0x0400C047 RID: 49223
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B111]</smallcaps>\n\n[LOG BEGINS]\n\n[A throat clears.]\n\nB111: We are now reliably printing healthy, living subjects, though all have exhibited unusual qualities as a result of the cloning process.\n\n[Trilling sounds can be heard.]\n\nB111: Unusual mewings, benign neoplasms, and atavistic extremities have been seen in all subjects thus far, to varying degrees of severity. It seems that bypassing or accelerating juvenility halts certain critical stages of development. Brain function, however, appears typical.\n\n[Trilling.]\n\nB111: T-They also seem quite happy.\n\nB111: Dr. Broussard, signing off.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200226B RID: 8811
		public class A1_CLONEDRATS
		{
			// Token: 0x04009C5A RID: 40026
			public static LocString TITLE = "Initial Success";

			// Token: 0x04009C5B RID: 40027
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C69 RID: 11369
			public class BODY
			{
				// Token: 0x0400C048 RID: 49224
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B111]</smallcaps>\n\n[LOG BEGINS]\n\n[A throat clears.]\n\nB111: We are now reliably printing healthy, living subjects, though all have exhibited unusual qualities as a result of the cloning process.\n\n[Squeaking sounds can be heard.]\n\nB111: Unusual vocalizations, benign growths, and missing appendages have been seen in all subjects thus far, to varying degrees of severity. It seems that bypassing or accelerating juvenility halts certain critical stages of development. Brain function, however, appears typical.\n\n[Squeaking.]\n\nB111: T-They also seem quite happy.\n\nB111: Dr. Broussard, signing off.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200226C RID: 8812
		public class A5_GENETICOOZE
		{
			// Token: 0x04009C5C RID: 40028
			public static LocString TITLE = "Biofluid";

			// Token: 0x04009C5D RID: 40029
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C6A RID: 11370
			public class BODY
			{
				// Token: 0x0400C049 RID: 49225
				public static LocString CONTAINER1 = "[LOG BEGINS]\n\nThe Printing Pod is primed by a synthesized bio-organic concoction the technicians have taken to calling \"Ooze\", a specialized mixture composed of water, carbon, and dozens upon dozens of the trace elements necessary for the creation of life.\n\nThe pod reconstitutes these elements into a living organism using the blueprints we feed it, before finally administering a shock of life.\n\nIt is like any other 3D printer. We just use different ink.\n\nDr. Broussard, signing off.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200226D RID: 8813
		public class A4_HIBISCUS3
		{
			// Token: 0x04009C5E RID: 40030
			public static LocString TITLE = "Experiment 7D";

			// Token: 0x04009C5F RID: 40031
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C6B RID: 11371
			public class BODY
			{
				// Token: 0x0400C04A RID: 49226
				public static LocString CONTAINER1 = "EXPERIMENT 7D\nSecurity Code: B111\n\nSubject: #762, \"Hibiscus-3\"\nAdult female, 42cm, 257g\n\nDonor: #650, \"Hibiscus\"\nAdult female, 42cm, 257g";

				// Token: 0x0400C04B RID: 49227
				public static LocString CONTAINER2 = "Hypothesis: Subjects cloned from Hibiscus will correctly operate a lever apparatus when introduced, demonstrating retention of original donor's conditioned memories.\n\nDonor subject #650, \"Hibiscus\", conditioned to pull a lever to the right for a reward (almonds). Conditioning took place over a period of two weeks.\n\nHibiscus quickly learned that pulling the lever to the left produced no results, and was reliably demonstrating the desired behavior by the end of the first week.\n\nTraining continued for one additional week to strengthen neural pathways and ensure the intended behavioral conditioning was committed to long term and muscle memory.\n\nCloning subject #762, \"Hibiscus-3\", was introduced to the lever apparatus to ascertain memory retention and recall.\n\nHibiscus-3 showed no signs of recognition and did not perform the desired behavior. Subject initially failed to interact with the apparatus on any level.\n\nOn second introduction, Hibiscus-3 pulled the lever to the left.\n\nConclusion: Printed subject retains no memory from donor.";
			}
		}

		// Token: 0x0200226E RID: 8814
		public class A3_HUSBANDRYNOTES
		{
			// Token: 0x04009C60 RID: 40032
			public static LocString TITLE = "Husbandry Notes";

			// Token: 0x04009C61 RID: 40033
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C6C RID: 11372
			public class BODY
			{
				// Token: 0x0400C04C RID: 49228
				public static LocString CONTAINER1 = "<smallcaps>[Log Fragmentation Detected]\n[Voice Recognition Unavailable]</smallcaps>\n\n[LOG BEGINS]\n\n...The Hatch has been selected for development due to its naturally wide range of potential food sources.\n\nEnergy production is our primary goal, but augmentation to allow for the consumption of non-organic materials is a more attainable first step, and will have additional uses for waste disposal...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C04D RID: 49229
				public static LocString CONTAINER2 = "[LOG BEGINS]\n\n...The Morb has been selected for development based on its ability to perform a multitude of the waste breakdown functions typical for a healthy ecosystem.\n\nDesign should focus on eliminating the disease risks posed by a fully matured Morb specimen...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C04E RID: 49230
				public static LocString CONTAINER3 = "[LOG BEGINS]\n\n...The Puft may be suited for serving a sustainable decontamination role.\n\nPotential design must focus on the efficiency of these processes...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C04F RID: 49231
				public static LocString CONTAINER4 = "[LOG BEGINS]\n\n...Wheezeworts are an ideal selection due to their low nutrient requirements and natural terraforming capabilities.\n\nDesign of these creatures should focus on enhancing their natural influence on ambient temperatures...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C050 RID: 49232
				public static LocString CONTAINER5 = "[LOG BEGINS]\n\n...The preliminary Hatch gene splices were successful.\n\nThe prolific mucus excretions that are typical of the species are now producing hydrocarbons at an incredible pace.\n\nThe creature has essentially become a free source of burnable oil...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C051 RID: 49233
				public static LocString CONTAINER6 = "[LOG BEGINS]\n\n...Bioluminescence is always a novelty, but little time should be spent on perfecting these insects from here on out.\n\nThe project has more pressing concerns than light sources, particularly now that the low light vegetation issue has been solved...\n\n[LOG ENDS]\n------------------\n";

				// Token: 0x0400C052 RID: 49234
				public static LocString CONTAINER7 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: B363]</smallcaps>\n\n[LOG BEGINS]\n\nGeneticist: The primary concern raised by this project is the variability of environments that colonies may be forced to deal with. The creatures we send with the settlement party will not have the time to evolve and adapt to a new environment, yet each creature has been chosen to play a vital role in colony sustainability and is thus too precious to risk loss.\n\nGeneticist: It follows that each organism we design must be equipped with the tools to survive in as many volatile environments as we are capable of planning for. We should not rely on the Pod alone to replenish creature populations.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x0200226F RID: 8815
		public class A6_MEMORYIMPLANTATION
		{
			// Token: 0x04009C62 RID: 40034
			public static LocString TITLE = "Memory Dysfunction Log";

			// Token: 0x04009C63 RID: 40035
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: TWO";

			// Token: 0x02002C6D RID: 11373
			public class BODY
			{
				// Token: 0x0400C053 RID: 49235
				public static LocString CONTAINER1 = "[LOG BEGINS]\n\nTraditionally, cloning produces a subject that is genetically identical to the donor but develops independently, producing a being that is, in its own way, unique.\n\nThe pod, conversely, attempts to print an exact atomic copy. Theoretically all neural pathways should be intact and identical to the original subject.\n\nIt's fascinating, given this, that memories are not already inherent in our subjects; however, no cloned subjects as of yet have shown any signs of recognition when introduced to familiar stimuli, such as the donor subject's enclosure.\n\nRefer to Experiment 7D.\n\nRefer to Experiment 7F.";

				// Token: 0x0400C054 RID: 49236
				public static LocString CONTAINER2 = "\nMemories <i>must</i> be embedded within the physical brainmaps of our subjects. The only question that remains is how to activate them. Hormones? Chemical supplements? Situational triggers?\n\nThe Director seems eager to move past this problem, and I am concerned at her willingness to bypass essential stages of the research development process.\n\nWe cannot move on to the fine polish of printing systems until the core processes have been perfected - which they have not.\n\nDr. Broussard, signing off.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002270 RID: 8816
		public class B9_TELEPORTATION
		{
			// Token: 0x04009C64 RID: 40036
			public static LocString TITLE = "Memory Breakthrough";

			// Token: 0x04009C65 RID: 40037
			public static LocString SUBTITLE = "ENCRYPTION LEVEL: THREE";

			// Token: 0x02002C6E RID: 11374
			public class BODY
			{
				// Token: 0x0400C055 RID: 49237
				public static LocString CONTAINER1 = "<smallcaps>[Voice Recognition Initialized]\n[Subject Identified: A001]</smallcaps>\n\n[LOG BEGINS]\n\nDr. Techna's newest notes on Duplicant memories have revealed some interesting discoveries. It seems memories </i>can</i> be transferred to the cloned subject but it requires the host to be subjected to a machine that performs extremely detailed microanalysis. This in-depth dissection of the subject would produce the results we need but at the expense of destroying the host.\n\nOf course this is not ideal for our current situation. The time and energy it took to recruit Gravitas' highly trained staff would be wasted if we were to extirpate these people for the sake of experimentation. But perhaps we can use our Duplicants as experimental subjects until we perfect the process and look into finding volunteers for the future in order to obtain an ideal specimen. I will have to discuss this with Dr. Techna but I'm sure he would be enthusiastic about such an opportunity to continue his work.\n\nI am also very interested in the commercial opportunities this presents. Off the top of my head I can think of applications in genetics, AI development, and teleportation technology. This could be a significant financial windfall for the company.\n\n[LOG ENDS]";
			}
		}

		// Token: 0x02002271 RID: 8817
		public class AUTOMATION
		{
			// Token: 0x04009C66 RID: 40038
			public static LocString TITLE = UI.FormatAsLink("Automation", "LOGIC");

			// Token: 0x04009C67 RID: 40039
			public static LocString HEADER_1 = "Automation";

			// Token: 0x04009C68 RID: 40040
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				"Automation is a tool for controlling the operation of buildings based on what sensors in the colony are detecting.\n\nA ",
				BUILDINGS.PREFABS.CEILINGLIGHT.NAME,
				" could be configured to automatically turn on when a ",
				BUILDINGS.PREFABS.LOGICDUPLICANTSENSOR.NAME,
				" detects a Duplicant in the room.\n\nA ",
				BUILDINGS.PREFABS.LIQUIDPUMP.NAME,
				" might activate only when a ",
				BUILDINGS.PREFABS.LOGICELEMENTSENSORLIQUID.NAME,
				" detects water.\n\nA ",
				BUILDINGS.PREFABS.AIRCONDITIONER.NAME,
				" might activate only when the ",
				BUILDINGS.PREFABS.LOGICTEMPERATURESENSOR.NAME,
				" detects too much heat.\n\n"
			});

			// Token: 0x04009C69 RID: 40041
			public static LocString HEADER_2 = "Automation Wires";

			// Token: 0x04009C6A RID: 40042
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"In addition to an ",
				UI.FormatAsLink("electrical wire", "WIRE"),
				", most powered buildings can also have an ",
				BUILDINGS.PREFABS.LOGICWIRE.NAME,
				" connected to them. This wire can signal the building to turn on or off. If the other end of a ",
				BUILDINGS.PREFABS.LOGICWIRE.NAME,
				" is connected to a sensor, the building will turn on and off as the sensor outputs signals.\n\n"
			});

			// Token: 0x04009C6B RID: 40043
			public static LocString HEADER_3 = "Signals";

			// Token: 0x04009C6C RID: 40044
			public static LocString PARAGRAPH_3 = string.Concat(new string[]
			{
				"There are two signals that an ",
				BUILDINGS.PREFABS.LOGICWIRE.NAME,
				" can send: Green and Red. The green signal will usually cause buildings to turn on, and the red signal will usually cause buildings to turn off. Sensors can often be configured to send their green signal only under certain conditions. A ",
				BUILDINGS.PREFABS.LOGICTEMPERATURESENSOR.NAME,
				" could be configured to only send a green signal if detecting temperatures greater than a chosen value.\n\n"
			});

			// Token: 0x04009C6D RID: 40045
			public static LocString HEADER_4 = "Gates";

			// Token: 0x04009C6E RID: 40046
			public static LocString PARAGRAPH_4 = "The signals of sensor wires can be combined using special buildings called \"Gates\" in order to create complex activation conditions.\nThe " + BUILDINGS.PREFABS.LOGICGATEAND.NAME + " can have two automation wires connected to its input slots, and one connected to its output slots. It will send a \"Green\" signal to its output slot only if it is receiving a \"Green\" signal from both its input slots. This could be used to activate a building only when multiple sensors are detecting something.\n\n";
		}

		// Token: 0x02002272 RID: 8818
		public class DECORSYSTEM
		{
			// Token: 0x04009C6F RID: 40047
			public static LocString TITLE = UI.FormatAsLink("Decor", "DECOR");

			// Token: 0x04009C70 RID: 40048
			public static LocString HEADER_1 = "Decor";

			// Token: 0x04009C71 RID: 40049
			public static LocString PARAGRAPH_1 = "Low Decor can increase Duplicant " + UI.FormatAsLink("Stress", "STRESS") + ". Thankfully, pretty things tend to increase the Decor value of an area. Each Duplicant has a different idea of what is a high enough Decor value. If the average Decor that a Duplicant experiences in a cycle is below their expectations, they will suffer a stress penalty.\n\n";

			// Token: 0x04009C72 RID: 40050
			public static LocString HEADER_2 = "Calculating Decor";

			// Token: 0x04009C73 RID: 40051
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"Many things have an effect on the Decor value of a tile. A building's effect is expressed as a strength value and a radius. Often that effect is positive, but many buildings also lower the decor value of an area too. ",
				UI.FormatAsLink("Plants", "PLANTS"),
				", ",
				UI.FormatAsLink("Critters", "CREATURES"),
				", and ",
				UI.FormatAsLink("Furniture", "BUILDCATEGORYFURNITURE"),
				" often increase decor while industrial buildings, debris, and rot often decrease it. Duplicants experience the combined decor of all objects affecting a tile.\n\nThe ",
				CREATURES.SPECIES.PRICKLEGRASS.NAME,
				" has a decor value of ",
				string.Format("{0} and a radius of {1} tiles. ", PrickleGrassConfig.POSITIVE_DECOR_EFFECT.amount, PrickleGrassConfig.POSITIVE_DECOR_EFFECT.radius),
				"\nThe ",
				BUILDINGS.PREFABS.MICROBEMUSHER.NAME,
				" has a decor value of ",
				string.Format("{0} and a radius of {1} tiles. ", MicrobeMusherConfig.DECOR.amount, MicrobeMusherConfig.DECOR.radius),
				"\nThe result of placing a ",
				BUILDINGS.PREFABS.MICROBEMUSHER.NAME,
				" next to a ",
				CREATURES.SPECIES.PRICKLEGRASS.NAME,
				" would be a combined decor value of ",
				(MicrobeMusherConfig.DECOR.amount + PrickleGrassConfig.POSITIVE_DECOR_EFFECT.amount).ToString(),
				"."
			});
		}

		// Token: 0x02002273 RID: 8819
		public class EXOBASES
		{
			// Token: 0x04009C74 RID: 40052
			public static LocString TITLE = UI.FormatAsLink("Space Travel", "EXOBASES");

			// Token: 0x04009C75 RID: 40053
			public static LocString HEADER_1 = "Building Rockets";

			// Token: 0x04009C76 RID: 40054
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				"Building a rocket first requires constructing a ",
				UI.FormatAsLink("Rocket Platform", "LAUNCHPAD"),
				" and adding modules from the menu. All rockets will require an engine, a nosecone and a Command Module piloted by a Duplicant possessing the ",
				UI.FormatAsLink("Rocket Piloting", "ROCKETPILOTING1"),
				" skill or higher. Note that the ",
				UI.FormatAsLink("Solo Spacefarer Nosecone", "HABITATMODULESMALL"),
				" functions as both a Command Module and a nosecone.\n\n"
			});

			// Token: 0x04009C77 RID: 40055
			public static LocString HEADER_2 = "Space Travel";

			// Token: 0x04009C78 RID: 40056
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"To scan space and see nearby intersteller destinations a ",
				UI.FormatAsLink("Telescope", "CLUSTERTELESCOPE"),
				" must first be built on the surface of a Planetoid. ",
				UI.FormatAsLink("Orbital Data Collection Lab", "ORBITALRESEARCHCENTER"),
				" in orbit around a Planetoid, and ",
				UI.FormatAsLink("Cartographic Module", "SCANNERMODULE"),
				" attached to a rocket can also reveal places on a Starmap.\n\nAlways check engine fuel to determine if your rocket can reach its destination, keeping in mind rockets can only land on Plantoids with a ",
				UI.FormatAsLink("Rocket Platform", "LAUNCHPAD"),
				" on it although some modules like ",
				UI.FormatAsLink("Rover's Modules", "SCOUTMODULE"),
				" and ",
				UI.FormatAsLink("Trailblazer Modules", "PIONEERMODULE"),
				" can be sent to the surface of a Planetoid from a rocket in orbit.\n\n"
			});

			// Token: 0x04009C79 RID: 40057
			public static LocString HEADER_3 = "Space Transport";

			// Token: 0x04009C7A RID: 40058
			public static LocString PARAGRAPH_3 = string.Concat(new string[]
			{
				"Goods can be teleported between worlds with connected Supply Teleporters through ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				", ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				", and ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" conduits.\n\nPlanetoids not connected through Supply Teleporters can use rockets to transport goods, either by landing on a ",
				UI.FormatAsLink("Rocket Platform", "LAUNCHPAD"),
				" or a ",
				UI.FormatAsLink("Orbital Cargo Module", "ORBITALCARGOMODULE"),
				" deployed from a rocket in orbit. Additionally, the ",
				UI.FormatAsLink("Interplanetary Launcher", "RAILGUN"),
				" can send ",
				UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
				" full of goods through space but must be opened by a ",
				UI.FormatAsLink("Payload Opener", "RAILGUNPAYLOADOPENER"),
				". A ",
				UI.FormatAsLink("Targeting Beacon", "LANDINGBEACON"),
				" can guide payloads and orbital modules to land at a specific location on a Planetoid surface."
			});
		}

		// Token: 0x02002274 RID: 8820
		public class GENETICS
		{
			// Token: 0x04009C7B RID: 40059
			public static LocString TITLE = UI.FormatAsLink("Genetics", "GENETICS");

			// Token: 0x04009C7C RID: 40060
			public static LocString HEADER_1 = "Plant Mutations";

			// Token: 0x04009C7D RID: 40061
			public static LocString PARAGRAPH_1 = "Plants exposed to radiation sometimes drop mutated seeds when they are harvested. Each type of mutation has its own efficiencies and trade-offs.\n\nMutated seeds can be planted once they have been analyzed in the " + UI.FormatAsLink("Botanical Analyzer", "GENETICANALYSISSTATION") + ", but the resulting plants will produce no seeds of their own unless they are uprooted.\n\n";

			// Token: 0x04009C7E RID: 40062
			public static LocString HEADER_2 = "Cultivating Mutated Seeds";

			// Token: 0x04009C7F RID: 40063
			public static LocString PARAGRAPH_2 = "Once mutated seeds have been analyzed in the Botanical Analyzer, they are ready to be planted. Continued exposure to naturally occurring radiation or a " + UI.FormatAsLink("Radiation Lamp", "RADIATIONLIGHT") + " is necessary to prevent wilting.\n\n";
		}

		// Token: 0x02002275 RID: 8821
		public class HEALTH
		{
			// Token: 0x04009C80 RID: 40064
			public static LocString TITLE = UI.FormatAsLink("Health", "HEALTH");

			// Token: 0x04009C81 RID: 40065
			public static LocString HEADER_1 = "Health";

			// Token: 0x04009C82 RID: 40066
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				"Duplicants can be physically damaged by some rare circumstances, such as extreme ",
				UI.FormatAsLink("Heat", "HEAT"),
				" or aggressive ",
				UI.FormatAsLink("Critters", "CREATURES"),
				". Damaged Duplicants will suffer greatly reduced athletic abilities, and are at risk of incapacitation if damaged too severely.\n\n"
			});

			// Token: 0x04009C83 RID: 40067
			public static LocString HEADER_2 = "Incapacitation and Death";

			// Token: 0x04009C84 RID: 40068
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"Incapacitated Duplicants cannot move or perform errands. They must be rescued by another Duplicant before their health drops to zero. If a Duplicant's health reaches zero they will die.\n\nHealth can be restored slowly over time and quickly through rest at the ",
				BUILDINGS.PREFABS.MEDICALCOT.NAME,
				".\n\n Duplicants are generally more vulnerable to ",
				UI.FormatAsLink("Disease", "DISEASE"),
				" than physical damage.\n\n"
			});

			// Token: 0x04009C85 RID: 40069
			public static LocString HEADER_3 = "Sleep and Stamina";

			// Token: 0x04009C86 RID: 40070
			public static LocString PARAGRAPH_3 = string.Concat(new string[]
			{
				"Sleep deprivation increases ",
				UI.FormatAsLink("Stress", "STRESS"),
				" and depletes stamina. When stamina reaches zero, exhausted Duplicants will pass out from fatigue.\n\nEach Duplicant should be assigned a ",
				UI.FormatAsLink("Bed", "BED"),
				" of their own, and have adequate time ",
				UI.FormatAsLink("scheduled", "MISCELLANEOUSTIPS14"),
				" for rest.\n\n"
			});
		}

		// Token: 0x02002276 RID: 8822
		public class HEAT
		{
			// Token: 0x04009C87 RID: 40071
			public static LocString TITLE = UI.FormatAsLink("Heat", "HEAT");

			// Token: 0x04009C88 RID: 40072
			public static LocString HEADER_1 = "Temperature";

			// Token: 0x04009C89 RID: 40073
			public static LocString PARAGRAPH_1 = "Just about everything on the asteroid has a temperature. It's normal for temperature to rise and fall a bit, but extreme temperatures can cause all sorts of problems for a base. Buildings can stop functioning, crops can wilt, and things can even melt, boil, and freeze when they really ought not to.\n\n";

			// Token: 0x04009C8A RID: 40074
			public static LocString HEADER_2 = "Wilting, Overheating, and Melting";

			// Token: 0x04009C8B RID: 40075
			public static LocString PARAGRAPH_2 = "Most crops require their body temperatures to be within a certain range in order to grow. Values outside of this range are not fatal, but will pause growth. If a building's temperature exceeds its overheat temperature it will take damage and require repair.\nAt very extreme temperatures buildings may melt or boil away.\n\n";

			// Token: 0x04009C8C RID: 40076
			public static LocString HEADER_3 = "Thermal Energy";

			// Token: 0x04009C8D RID: 40077
			public static LocString PARAGRAPH_3 = "Temperature increase when the thermal energy of a substance increases. The value of temperature is equal to the total Thermal Energy divided by the Specific Heat Capacity of the substance. Because Specific Heat Capacity varies between substances so significantly, it is often the case a substance can have a higher temperature than another despite a lower overall thermal energy. This quality makes Water require nearly four times the amount of thermal energy to increase in temperature compared to Oxygen.\n\n";

			// Token: 0x04009C8E RID: 40078
			public static LocString HEADER_4 = "Conduction and Insulation";

			// Token: 0x04009C8F RID: 40079
			public static LocString PARAGRAPH_4 = "Thermal energy can be transferred between Buildings, Creatures, World tiles, and other world entities through Conduction. Conduction occurs when two things of different Temperatures are touching. The rate of the energy transfer is the product of the averaged Conductivity values and Temperature difference. Thermal energy will flow slowly between substances with low conductivity values (insulators), and quickly between substances with high conductivity (conductors).\n\n";

			// Token: 0x04009C90 RID: 40080
			public static LocString HEADER_5 = "State Changes";

			// Token: 0x04009C91 RID: 40081
			public static LocString PARAGRAPH_5 = "Water ice melts into liquid water when its temperature rises above its melting point. Liquid water boils into steam when its temperature rises above its boiling point. Similar transitions in state occur for most elements, but each element has its own threshold temperatures. Sometimes the transitions are not reversible - crude oil boiled into sour gas will not condense back to crude oil when cooled. Instead, the substance might condense into a totally different element with a different utility. \n\n";
		}

		// Token: 0x02002277 RID: 8823
		public class LIGHT
		{
			// Token: 0x04009C92 RID: 40082
			public static LocString TITLE = UI.FormatAsLink("Light", "LIGHT");

			// Token: 0x04009C93 RID: 40083
			public static LocString HEADER_1 = "Light";

			// Token: 0x04009C94 RID: 40084
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				"Most of the asteroid is dark. Light sources such as the ",
				BUILDINGS.PREFABS.CEILINGLIGHT.NAME,
				" or ",
				CREATURES.SPECIES.LIGHTBUG.NAME,
				" improves Decor and gives Duplicants a boost to their productivity. Many plants are also sensitive to the amount of light they receive.\n\n"
			});

			// Token: 0x04009C95 RID: 40085
			public static LocString HEADER_2 = "Light Sources";

			// Token: 0x04009C96 RID: 40086
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"The ",
				BUILDINGS.PREFABS.FLOORLAMP.NAME,
				" and ",
				BUILDINGS.PREFABS.CEILINGLIGHT.NAME,
				" produce a decent amount of light when powered. The ",
				CREATURES.SPECIES.LIGHTBUG.NAME,
				" naturally emits a halo of light. Strong solar light is available on the surface during daytime.\n\n"
			});

			// Token: 0x04009C97 RID: 40087
			public static LocString HEADER_3 = "Measuring Light";

			// Token: 0x04009C98 RID: 40088
			public static LocString PARAGRAPH_3 = string.Concat(new string[]
			{
				"The amount of light on a cell is measured in Lux. Lux has a dramatic range - A simple ",
				BUILDINGS.PREFABS.CEILINGLIGHT.NAME,
				" produces ",
				1800.ToString(),
				" Lux, while the sun can produce values as high as ",
				80000.ToString(),
				" Lux. The ",
				BUILDINGS.PREFABS.SOLARPANEL.NAME,
				" generates power proportional to how many Lux it is exposed to.\n\n"
			});
		}

		// Token: 0x02002278 RID: 8824
		public class MORALE
		{
			// Token: 0x04009C99 RID: 40089
			public static LocString TITLE = UI.FormatAsLink("Morale", "MORALE");

			// Token: 0x04009C9A RID: 40090
			public static LocString HEADER_1 = "Morale";

			// Token: 0x04009C9B RID: 40091
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				"Morale describes the relationship between a Duplicant's ",
				UI.FormatAsLink("Skills", "ROLES"),
				" and their Lifestyle. The more skills a Duplicant has, the higher their morale expectation will be. Duplicants with morale below their expectation will experience a ",
				UI.FormatAsLink("Stress", "STRESS"),
				" penalty. Comforts such as quality ",
				UI.FormatAsLink("Food", "FOOD"),
				", nice rooms, and recreation will increase morale.\n\n"
			});

			// Token: 0x04009C9C RID: 40092
			public static LocString HEADER_2 = "Recreation";

			// Token: 0x04009C9D RID: 40093
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"Recreation buildings such as the ",
				BUILDINGS.PREFABS.WATERCOOLER.NAME,
				" and ",
				BUILDINGS.PREFABS.ESPRESSOMACHINE.NAME,
				" improve a Duplicant's morale when used. Duplicants need downtime time in their schedules to use these buildings.\n\n"
			});

			// Token: 0x04009C9E RID: 40094
			public static LocString HEADER_3 = "Overjoyed Responses";

			// Token: 0x04009C9F RID: 40095
			public static LocString PARAGRAPH_3 = "If a Duplicant has a very high Morale value, they will spontaneously display an Overjoyed Response. Each Duplicant has a different Overjoyed Behavior - but all overjoyed responses are good. Some will positively affect Building " + UI.FormatAsLink("Decor", "DECOR") + ", others will positively affect Duplicant morale or productivity.\n\n";
		}

		// Token: 0x02002279 RID: 8825
		public class POWER
		{
			// Token: 0x04009CA0 RID: 40096
			public static LocString TITLE = UI.FormatAsLink("Power", "POWER");

			// Token: 0x04009CA1 RID: 40097
			public static LocString HEADER_1 = "Electricity";

			// Token: 0x04009CA2 RID: 40098
			public static LocString PARAGRAPH_1 = "Electrical power is required to run many of the buildings in a base. Different buildings requires different amounts of power to run. Power can be transferred to buildings that require it using " + UI.FormatAsLink("Wires", "WIRE") + ".\n\n";

			// Token: 0x04009CA3 RID: 40099
			public static LocString HEADER_2 = "Generators and Batteries";

			// Token: 0x04009CA4 RID: 40100
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"Several buildings can generate power. Duplicants can run on the ",
				BUILDINGS.PREFABS.MANUALGENERATOR.NAME,
				" to generate clean power. Once generated, power can be consumed by buildings or stored in a ",
				BUILDINGS.PREFABS.BATTERY.NAME,
				" to prevent waste. Any generated power that is not consumed or stored will be wasted. Batteries and Generators tend to produce a significant amount of ",
				UI.FormatAsLink("Heat", "HEAT"),
				" while active.\n\nPower can also be stored in portable ",
				UI.FormatAsLink("Power Banks", "ELECTROBANK"),
				".\n\n"
			});

			// Token: 0x04009CA5 RID: 40101
			public static LocString HEADER_3 = "Measuring Power";

			// Token: 0x04009CA6 RID: 40102
			public static LocString PARAGRAPH_3 = "Power is measure in Joules when stored in a " + BUILDINGS.PREFABS.BATTERY.NAME + ". Power produced and consumed by buildings is measured in Watts, which are equal to Joules (consumed or produced) per second.\n\nA Battery that stored 5000 Joules could power a building that consumed 240 Watts for about 20 seconds. A generator which produces 480 Watts could power two buildings which consume 240 Watts for as long as it was running.\n\n";

			// Token: 0x04009CA7 RID: 40103
			public static LocString HEADER_4 = "Overloading";

			// Token: 0x04009CA8 RID: 40104
			public static LocString PARAGRAPH_4 = string.Concat(new string[]
			{
				"A network of ",
				UI.FormatAsLink("Wires", "WIRE"),
				" can be overloaded if it is consuming too many watts. If the wattage of a wire network exceeds its limits it may break and require repair.\n\n",
				UI.FormatAsLink("Standard wires", "WIRE"),
				" have a ",
				1000.ToString(),
				" Watt limit.\n\n"
			});
		}

		// Token: 0x0200227A RID: 8826
		public class PRIORITY
		{
			// Token: 0x04009CA9 RID: 40105
			public static LocString TITLE = UI.FormatAsLink("Priorities", "PRIORITY");

			// Token: 0x04009CAA RID: 40106
			public static LocString HEADER_1 = "Errand Priority";

			// Token: 0x04009CAB RID: 40107
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				"Duplicants prioritize their errands based on several factors. Some of these can be adjusted to affect errand choice, but some errands (such as seeking breathable ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				") are so important that they cannot be delayed. Errand priority can primarily be controlled by Errand Type prioritization, and then can be further fine-tuned by the ",
				UI.FormatAsTool("Priority Tool", global::Action.Prioritize),
				".\n\n"
			});

			// Token: 0x04009CAC RID: 40108
			public static LocString HEADER_2 = "Errand Type Prioritization";

			// Token: 0x04009CAD RID: 40109
			public static LocString PARAGRAPH_2 = "Each errand a Duplicant can perform falls into an Errand Category. These categories can be prioritized on a per-Duplicant basis in the " + UI.FormatAsManagementMenu("Priorities Screen") + ". Entire errand categories can also be prohibited to a Duplicant if they are meant to never perform errands of that variety. A common configuration is to assign errand type priority based on Duplicant attributes.\n\nFor example, Duplicants who are good at Research could be made to prioritize the Researching errand type. Duplicants with poor Athletics could be made to deprioritize the Supplying and Storing errand types.\n\n";

			// Token: 0x04009CAE RID: 40110
			public static LocString HEADER_3 = "Priority Tool";

			// Token: 0x04009CAF RID: 40111
			public static LocString PARAGRAPH_3 = "The priority of errands can often be modified using the " + UI.FormatAsTool("Priority tool", global::Action.Prioritize) + ". The values applied by this tool are always less influential than the Errand Type priorities described above. If two errands with equal Errand Type Priority are available to a Duplicant, they will choose the errand with a higher priority setting as applied by the tool.\n\n";
		}

		// Token: 0x0200227B RID: 8827
		public class RADIATION
		{
			// Token: 0x04009CB0 RID: 40112
			public static LocString TITLE = UI.FormatAsLink("Radiation", "RADIATION");

			// Token: 0x04009CB1 RID: 40113
			public static LocString HEADER_1 = "Radiation";

			// Token: 0x04009CB2 RID: 40114
			public static LocString PARAGRAPH_1 = string.Concat(new string[]
			{
				"When transporting radioactive materials such as ",
				UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
				", care must be taken to avoid exposing outside objects to ",
				UI.FormatAsLink("Radioactive Contaminants", "RADIATIONSICKNESS"),
				".\n\nUsing proper transportation vessels, such as those which are lined with ",
				UI.FormatAsLink("Lead", "LEAD"),
				", is crucial to ensuring that Duplicants avoid ",
				UI.FormatAsLink("Radiation Sickness", "RADIATIONSICKNESS"),
				"."
			});

			// Token: 0x04009CB3 RID: 40115
			public static LocString HEADER_2 = "Radiation Sickness";

			// Token: 0x04009CB4 RID: 40116
			public static LocString PARAGRAPH_2 = string.Concat(new string[]
			{
				"Duplicants who are exposed to ",
				UI.FormatAsLink("Radioactive Contaminants", "RADIATIONSICKNESS"),
				" will need to wear protection or they risk coming down with ",
				UI.FormatAsLink("Radiation Sickness", "RADIATIONSICKNESS"),
				".\n\nSome Duplicants will have more of a natural resistance to radiation, but prolonged exposure will still increase their chances of becoming sick.\n\nConsuming ",
				UI.FormatAsLink("Rad Pills", "BASICRADPILL"),
				" or seafood such as ",
				UI.FormatAsLink("Cooked Seafood", "COOKEDFISH"),
				" or ",
				UI.FormatAsLink("Waterweed", "SEALETTUCE"),
				" increases a Duplicant's radiation resistance, but will not cure a Duplicant's ",
				UI.FormatAsLink("Radiation Sickness", "RADIATIONSICKNESS"),
				" once they have become infected.\n\nOn the other hand, exposure to radiation will kill ",
				UI.FormatAsLink("Food Poisoning", "FOODPOISONING"),
				", ",
				UI.FormatAsLink("Slimelung", "SLIMELUNG"),
				" and ",
				UI.FormatAsLink("Zombie Spores", "ZOMBIESPORES"),
				" on surfaces (including on Duplicants).\n\n"
			});

			// Token: 0x04009CB5 RID: 40117
			public static LocString HEADER_3 = "Nuclear Energy";

			// Token: 0x04009CB6 RID: 40118
			public static LocString PARAGRAPH_3 = string.Concat(new string[]
			{
				"A ",
				UI.FormatAsLink("Research Reactor", "NUCLEARREACTOR"),
				" will require ",
				UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
				" to run. Uranium can be enriched using a ",
				UI.FormatAsLink("Uranium Centrifuge", "URANIUMCENTRIFUGE"),
				".\n\nOnce supplied with ",
				UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
				", a ",
				UI.FormatAsLink("Research Reactors", "NUCLEARREACTOR"),
				" will create an enormous amount of ",
				UI.FormatAsLink("Heat", "HEAT"),
				" which can then be placed under a source of ",
				UI.FormatAsLink("Water", "WATER"),
				" to produce ",
				UI.FormatAsLink("Steam", "STEAM"),
				"and connected to a ",
				UI.FormatAsLink("Steam Turbine", "STEAMTURBINE2"),
				" to produce a considerable source of ",
				UI.FormatAsLink("Power", "POWER"),
				"."
			});
		}

		// Token: 0x0200227C RID: 8828
		public class RESEARCH
		{
			// Token: 0x04009CB7 RID: 40119
			public static LocString TITLE = UI.FormatAsLink("Research", "RESEARCH");

			// Token: 0x04009CB8 RID: 40120
			public static LocString HEADER_1 = "Research";

			// Token: 0x04009CB9 RID: 40121
			public static LocString PARAGRAPH_1 = "Doing research unlocks new types of buildings for the colony. Duplicants can perform research at the " + BUILDINGS.PREFABS.RESEARCHCENTER.NAME + ".\n\n";

			// Token: 0x04009CBA RID: 40122
			public static LocString HEADER_2 = "Research Tasks";

			// Token: 0x04009CBB RID: 40123
			public static LocString PARAGRAPH_2 = "A selected research task is completed once enough research points have been generated at the colony's research stations. Duplicants with high 'Science' attribute scores will generate research points faster than Duplicants with lower scores.\n\n";

			// Token: 0x04009CBC RID: 40124
			public static LocString HEADER_3 = "Research Types";

			// Token: 0x04009CBD RID: 40125
			public static LocString PARAGRAPH_3 = string.Concat(new string[]
			{
				"Advanced research tasks require special research stations to generate the proper kind of research points. These research stations often consume more advanced resources.\n\nUsing higher-level research stations also requires Duplicants to have learned higher level research ",
				UI.FormatAsLink("skills", "ROLES"),
				".\n\n",
				STRINGS.RESEARCH.TYPES.ALPHA.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.RESEARCHCENTER.NAME,
				"\n",
				STRINGS.RESEARCH.TYPES.BETA.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.ADVANCEDRESEARCHCENTER.NAME,
				"\n",
				STRINGS.RESEARCH.TYPES.GAMMA.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.COSMICRESEARCHCENTER.NAME,
				"\n\n"
			});
		}

		// Token: 0x0200227D RID: 8829
		public class RESEARCHDLC1
		{
			// Token: 0x04009CBE RID: 40126
			public static LocString TITLE = UI.FormatAsLink("Research", "RESEARCHDLC1");

			// Token: 0x04009CBF RID: 40127
			public static LocString HEADER_1 = "Research";

			// Token: 0x04009CC0 RID: 40128
			public static LocString PARAGRAPH_1 = "Doing research unlocks new types of buildings for the colony. Duplicants can perform research at the " + BUILDINGS.PREFABS.RESEARCHCENTER.NAME + ".\n\n";

			// Token: 0x04009CC1 RID: 40129
			public static LocString HEADER_2 = "Research Tasks";

			// Token: 0x04009CC2 RID: 40130
			public static LocString PARAGRAPH_2 = "A selected research task is completed once enough research points have been generated at the colonies research stations. Duplicants with high 'Science' attribute scores will generate research points faster than Duplicants with lower scores.\n\n";

			// Token: 0x04009CC3 RID: 40131
			public static LocString HEADER_3 = "Research Types";

			// Token: 0x04009CC4 RID: 40132
			public static LocString PARAGRAPH_3 = string.Concat(new string[]
			{
				"Advanced research tasks require special research stations to generate the proper kind of research points. These research stations often consume more advanced resources.\n\nUsing higher level research stations also requires Duplicants to have learned higher level research ",
				UI.FormatAsLink("skills", "ROLES"),
				".\n\n",
				STRINGS.RESEARCH.TYPES.ALPHA.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.RESEARCHCENTER.NAME,
				"\n",
				STRINGS.RESEARCH.TYPES.BETA.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.ADVANCEDRESEARCHCENTER.NAME,
				"\n",
				STRINGS.RESEARCH.TYPES.GAMMA.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.COSMICRESEARCHCENTER.NAME,
				"\n",
				STRINGS.RESEARCH.TYPES.DELTA.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.NUCLEARRESEARCHCENTER.NAME,
				"\n",
				STRINGS.RESEARCH.TYPES.ORBITAL.NAME,
				" is performed at the ",
				BUILDINGS.PREFABS.ORBITALRESEARCHCENTER.NAME,
				"\n\n"
			});
		}

		// Token: 0x0200227E RID: 8830
		public class STRESS
		{
			// Token: 0x04009CC5 RID: 40133
			public static LocString TITLE = UI.FormatAsLink("Stress", "STRESS");

			// Token: 0x04009CC6 RID: 40134
			public static LocString HEADER_1 = "Stress";

			// Token: 0x04009CC7 RID: 40135
			public static LocString PARAGRAPH_1 = "A Duplicant's experiences in the colony affect their stress level. Stress increases when they have negative experiences or unmet expectations. Stress decreases with time if " + UI.FormatAsLink("Morale", "MORALE") + " is satisfied. Duplicant behavior starts to change for the worse when stress levels get too high.\n\n";

			// Token: 0x04009CC8 RID: 40136
			public static LocString HEADER_2 = "Stress Responses";

			// Token: 0x04009CC9 RID: 40137
			public static LocString PARAGRAPH_2 = "If a Duplicant has very high stress values they will experience a Stress Response episode. Each Duplicant has a different Stress Behavior - but all stress responses are bad. After the stress behavior episode is done, the Duplicants stress will reset to a lower value. Though, if the factors causing the Duplicant's high stress are not corrected they are bound to have another stress response episode.\n\n";
		}
	}
}
