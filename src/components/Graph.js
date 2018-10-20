import React from "react";
import GraphView from "./GraphView";

export default class Graph extends React.Component {
    render() {
        return (
            <GraphView verts={this.props.verts} edges={this.props.edges.map(e => [ this.props.verts[e[0]], this.props.verts[e[1]] ])} />
        );
    }
}
