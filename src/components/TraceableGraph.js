import React from "react";
import GraphView from "./GraphView";
import "./TraceableGraph.css";

function vertMatch(v1, v2) {
    if (v1[0] === undefined) {
        return v1 === v2;
    } else {
        return v1[0] === v2[0] && v1[1] === v2[1];
    }
}

function edgeMatch(e1, e2) {
    return (vertMatch(e1[0], e2[0]) && vertMatch(e1[1], e2[1])) || (vertMatch(e1[0], e2[1]) && vertMatch(e1[1], e2[0]));
}

function edgeVertMatch(e, v1, v2) {
    return (vertMatch(e[0], v1) && vertMatch(e[1], v2)) || (vertMatch(e[0], v2) && vertMatch(e[1], v1));
}

export default class TraceableGraph extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            "history": [],
            "drag": null,
            "width": 1,
            "height": 1
        };
        this.handleVertexMouseDown = this.handleVertexMouseDown.bind(this);
        this.handleResize = this.handleResize.bind(this);
        this.handleMouseUp = this.handleMouseUp.bind(this);
        this.handleMouseOut = this.handleMouseOut.bind(this);
        this.handleMouseMove = this.handleMouseMove.bind(this);
        this.handleVertexMouseOver = this.handleVertexMouseOver.bind(this);
    }

    componentDidUpdate(prevProps) {
        if (prevProps.verts !== this.props.verts || prevProps.edges !== this.props.edges || prevProps.nonce !== this.props.nonce) {
            this.setState({
                "history": [],
                "drag": null
            }, () => {
                if (this.props.onGraphChange) {
                    this.props.onGraphChange(this.state.history);
                }
            });
        }
    }

    handleVertexMouseDown(v, ev) {
        if (this.state.history.length === 0) {
            this.setState({
                "history": [ { "v": v } ],
                "drag": v
            }, () => {
                if (this.props.onGraphChange) {
                    this.props.onGraphChange(this.state.history);
                }
            });
        } else {
            let i;
            for (i = this.state.history.length - 1; i >= 0; --i) {
                if (vertMatch(this.state.history[i].v, v)) {
                    break;
                }
            }
            if (i >= 0) {
                this.setState({
                    "history": this.state.history.slice(0, i + 1),
                    "drag": v
                }, () => {
                    if (this.props.onGraphChange) {
                        this.props.onGraphChange(this.state.history);
                    }
                });
            }
        }
    }

    handleMouseUp(ev) {
        this.setState({
            "drag": null
        });
    }

    handleMouseOut(ev) {
        this.handleMouseUp(ev);
    }

    handleMouseMove(ev) {
        if (this.state.drag) {
            this.setState({
                "drag": [ ev.clientX / this.state.width, ev.clientY / this.state.height ]
            });
        }
    }

    handleVertexMouseOver(v, ev) {
        if (this.state.drag) {
            const tip = this.state.history[this.state.history.length - 1].v;
            const e = this.props.edges.find(e2 => edgeVertMatch([ this.props.verts[e2[0]], this.props.verts[e2[1]] ], tip, v));
            if (e && this.state.history.findIndex(h => h.e && edgeMatch(e, h.e)) < 0) {
                if (this.props.hamiltonian && this.state.history.findIndex(h => vertMatch(h.v, v)) >= 0) {
                    return;
                }
                this.setState({
                    "history": this.state.history.concat([ { "v": v, "e": e }]),
                    "drag": v
                }, () => {
                    if (this.props.onGraphChange) {
                        this.props.onGraphChange(this.state.history);
                    }
                });
            }
        }
    }

    handleResize(w, h) {
        this.setState({
            "width": w,
            "height": h
        });
    }

    render() {
        return (
            <GraphView verts={this.props.verts.map(v => [ v[0], v[1], this.state.history.findIndex(h => vertMatch(h.v, v)) >= 0 ? "passed" : null ])}
                edges={this.props.edges.map(e => [ this.props.verts[e[0]], this.props.verts[e[1]], this.state.history.findIndex(h => h.e && edgeMatch(h.e, e)) >= 0 ? "passed" : null ])
                    .concat(this.state.drag ? [[ this.state.history[this.state.history.length - 1].v, this.state.drag, "passed" ]] : [])}
                className="traceable-graph" onResize={this.handleResize}
                onMouseUp={this.handleMouseUp} onMouseOut={this.handleMouseOut} onMouseMove={this.handleMouseMove}
                onVertexMouseDown={this.handleVertexMouseDown} onVertexMouseOver={this.handleVertexMouseOver} />
        );
    }
}
