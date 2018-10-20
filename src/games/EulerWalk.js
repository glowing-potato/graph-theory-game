import React from "react";
import Game from "../components/Game";
import TraceableGraph from "../components/TraceableGraph";
import data from "./EulerWalk.json";

export default class EulerCycle extends React.Component {
    render() {
        return (
            <Game>
                <TraceableGraph verts={data[this.props.match.params.level - 1].v} edges={data[this.props.match.params.level - 1].e} />
            </Game>
        );
    }
}
