$AppPath = "..\KnapsackProblem\bin\Debug\"
$DataPath = "..\Data\KnapsackProblem\"

& $AppPath"KnapsackProblem.exe" -p 	$DataPath"knap_4.inst.dat" `
									$DataPath"knap_10.inst.dat" `
									$DataPath"knap_15.inst.dat" `
									$DataPath"knap_20.inst.dat" `
									$DataPath"knap_22.inst.dat" `
									$DataPath"knap_25.inst.dat" `
									$DataPath"knap_27.inst.dat" `
								-r 	$DataPath"knap_4.sol.dat"  `
									$DataPath"knap_10.sol.dat"  `
									$DataPath"knap_15.sol.dat"  `
									$DataPath"knap_20.sol.dat"  `
									$DataPath"knap_22.sol.dat"  `
									$DataPath"knap_25.sol.dat"  `
									$DataPath"knap_27.sol.dat"  `
								-c -v