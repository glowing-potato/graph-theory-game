import React from "react";
import Game from "../components/Game";

export default class HamiltonianCycle extends React.Component {
    render() {
        return (
            <Game>
                Hamiltonian Cycle Problem #{this.props.match.params.level}
            </Game>
        );
    }
}
