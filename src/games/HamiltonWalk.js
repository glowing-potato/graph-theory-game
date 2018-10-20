import React from "react";

export default class HamiltonianCycle extends React.Component {
    render() {
        return (
            <div>
                Hamiltonian Cycle Problem #{this.props.match.params.level}
            </div>
        );
    }
}
