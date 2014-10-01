$AppPath = "C:\Users\jantk_000\Documents\GitHub\MI-PAA\KnapsackProblem\bin\Debug\"
$DataPath = "C:\Users\jantk_000\Documents\GitHub\MI-PAA\Data\KnapsackProblem\"

& $AppPath"KnapsackProblem.exe" -p 	$DataPath"knap_4.inst.dat" `
									$DataPath"knap_10.inst.dat" `
									$DataPath"knap_15.inst.dat" `
									$DataPath"knap_20.inst.dat" `
								-r 	$DataPath"knap_4.sol.dat"  `
									$DataPath"knap_10.sol.dat"  `
									$DataPath"knap_15.sol.dat"  `
									$DataPath"knap_20.sol.dat"  `
								-a -b -v