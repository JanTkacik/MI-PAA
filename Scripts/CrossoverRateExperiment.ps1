$AppPath = "..\KnapsackProblem\bin\Debug\"
$DataPath = "..\Data\KnapsackProblem\"

& $AppPath"KnapsackProblem.exe" -p 	$DataPath"knap_30.inst.dat" `
									$DataPath"knap_32.inst.dat" `
									$DataPath"knap_35.inst.dat" `
									$DataPath"knap_37.inst.dat" `
									$DataPath"knap_40.inst.dat" `
								-r 	$DataPath"knap_30.sol.dat"  `
									$DataPath"knap_32.sol.dat"  `
									$DataPath"knap_35.sol.dat"  `
									$DataPath"knap_37.sol.dat"  `
									$DataPath"knap_40.sol.dat"  `
								-g -i 200 -m 0.82 -n 0.12 -q rank -t 100 -w 0.03 