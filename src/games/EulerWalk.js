import React from "react";
import Game from "../components/Game";
import TraceableGraph from "../components/TraceableGraph";
import data from "./EulerWalk.json";

export default class EulerCycle extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            "won": false,
            "nonce": -1
        };
        this.evaluateWin = this.evaluateWin.bind(this);
        this.handleReload = this.handleReload.bind(this);
    }

    componentDidUpdate(lastProps) {
        if (lastProps.match !== this.props.match) {
            this.setState({
                "won": false
            });
        }
    }

    evaluateWin(history) {
        if (history.length === data[this.props.match.params.level - 1].e.length + 1) {
            this.setState({
                "won": true
            });
        }
    }

    handleReload(nonce) {
        this.setState({
            "nonce": nonce,
            "won": false
        });
    }

    render() {
        return (
            <Game won={this.state.won} onReload={this.handleReload}>
                <TraceableGraph verts={data[this.props.match.params.level - 1].v} edges={data[this.props.match.params.level - 1].e}
                    onGraphChange={this.evaluateWin} nonce={this.state.nonce} />
            </Game>
        );
    }
}
