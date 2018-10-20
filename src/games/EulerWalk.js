import React from "react";
import Game from "../components/Game";
import Graph from "../components/Graph";
import data from "./EulerWalk.json";

export default class EulerCycle extends React.Component {
    render() {
        return (
            <Game>
                <Graph verts={data[this.props.match.params.level - 1].v} edges={data[this.props.match.params.level - 1].e} />
            </Game>
        );
    }
}
