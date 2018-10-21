import EulerTrail from "../games/EulerTrail.json";
import HamiltonPath from "../games/HamiltonPath.json";
import EulerCycle from "../games/EulerCycle.json";
import HamiltonCircuit from "../games/HamiltonCircuit.json";

const LevelData = [
    { "id": "euler-trail", "name": "Euler Trails", "levels": EulerTrail.length },
    { "id": "hamilton-path", "name": "Hamiltonian Paths", "levels": HamiltonPath.length },
    { "id": "euler-cycle", "name": "Euler Cycles", "levels": EulerCycle.length },
    { "id": "hamilton-circuit", "name": "Hamiltonian Circuits", "levels": HamiltonCircuit.length }
];
export default LevelData;
