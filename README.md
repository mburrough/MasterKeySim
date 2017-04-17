# MasterKeySim
A simulation to determine how many co-conspirators are needed on average to determine a master key bitting through process of elimination. Assumes that no operator key can have any pins that collide with the master bitting.

Known Limitations
-----------------
1. The simulation only considers a basic lock system with one operator key and one master key. Things like multi-level-masters or multiple shear lines are not supported.
1. Maximum Adjacent Cut / Minimum Opposite Cut Specifications (MACS / MOCS) are not supported. 
1. There is no input validation on the variables to KeyTest. Program assumes you provide sane values.

**No guarantee is made to the accuracy of this calculation. This program is a hypothetical simulation, and not designed to assess any particular brand or model of real world lock/key system. To be used for entertainment purposes only.**

Sample Output
-------------
Sample output values (10,000 test runs each):
* 6 Pins / 6 Depths / 0 Buffer / Random Masters: 17.013
* 6 Pins / 6 Depths / 1 Buffer / Random Masters: 10.439
* 6 Pins / 6 Depths / 1 Buffer / Worst-Case Master: 12.757
* 7 Pins / 6 Depths / 0 Buffer / Random Masters: 18.108
* 7 Pins / 6 Depths / 1 Buffer / Random Masters: 10.687
* 7 Pins / 6 Depths / 1 Buffer / Worst-Case Master: 13.108

(Worst-case master is an all-minimum or all-maximum bitting, since half the buffer is unused.)

See also [this StackExchange thread](https://math.stackexchange.com/questions/2237744/how-long-does-it-take-to-guess-a-master-key).
