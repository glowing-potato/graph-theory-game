import React from "react";

export default class EulerCycle extends React.Component {
    render() {
        return (
            <div>
                Euler Cycle Problem #{this.props.match.params.level}
            </div>
        );
    }
}
