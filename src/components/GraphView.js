import React from "react";
import "./GraphView.css";

export default class GraphView extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            "width": 1,
            "height": 1
        };
        this.handleResize = this.handleResize.bind(this);
        this.handleRef = this.handleRef.bind(this);
        this.handleDrag = this.handleDrag.bind(this);
    }

    componentDidMount() {
        window.addEventListener("resize", this.handleResize);
    }

    componentWillUnmount() {
        window.removeEventListener("resize", this.handleResize);
    }

    handleResize() {
        this.setState({
            "width": this.view.clientWidth,
            "height": this.view.clientHeight
        });
        if (this.props.onResize) {
            this.props.onResize(this.view.clientWidth, this.view.clientHeight);
        }
    }

    handleRef(el) {
        this.view = el;
        this.handleResize();
    }

    handleDrag(ev) {
        ev.preventDefault();
    }

    handleVertexClick(v, ev) {
        if (this.props.onVertexClick) {
            this.props.onVertexClick(v, ev);
        }
    }

    handleVertexMouseOver(v, ev) {
        if (this.props.onVertexMouseOver) {
            this.props.onVertexMouseOver(v, ev);
        }
    }

    handleVertexMouseOut(v, ev) {
        if (this.props.onVertexMouseOut) {
            this.props.onVertexMouseOut(v, ev);
        }
    }

    handleVertexMouseDown(v, ev) {
        if (this.props.onVertexMouseDown) {
            this.props.onVertexMouseDown(v, ev);
        }
    }

    handleVertexMouseUp(v, ev) {
        if (this.props.onVertexMouseUp) {
            this.props.onVertexMouseUp(v, ev);
        }
    }

    handleVertexMouseMove(v, ev) {
        if (this.props.onVertexMouseMove) {
            this.props.onVertexMouseMove(v, ev);
        }
    }

    handleEdgeClick(v, ev) {
        if (this.props.onEdgeClick) {
            this.props.onEdgeClick(v, ev);
        }
    }

    handleEdgeMouseOver(v, ev) {
        if (this.props.onEdgeMouseOver) {
            this.props.onEdgeMouseOver(v, ev);
        }
    }

    handleEdgeMouseOut(v, ev) {
        if (this.props.onEdgeMouseOut) {
            this.props.onEdgeMouseOut(v, ev);
        }
    }

    handleEdgeMouseDown(v, ev) {
        if (this.props.onEdgeMouseDown) {
            this.props.onEdgeMouseDown(v, ev);
        }
    }

    handleEdgeMouseUp(v, ev) {
        if (this.props.onEdgeMouseUp) {
            this.props.onEdgeMouseUp(v, ev);
        }
    }

    handleEdgeMouseMove(v, ev) {
        if (this.props.onEdgeMouseMove) {
            this.props.onEdgeMouseMove(v, ev);
        }
    }

    render() {
        return (
            <div className={`graph-view ${this.props.className}`} ref={this.handleRef} onDrag={this.handleDrag}
                onClick={this.props.onClick} onMouseEnter={this.props.onMouseOver} onMouseLeave={this.props.onMouseOut}
                onMouseDown={this.props.onMouseDown} onMouseUp={this.props.onMouseUp} onMouseMove={this.props.onMouseMove}>
                {this.props.verts.map((v, i) => (
                    <div key={i} className={`vert ${v[2]}`} onDrag={this.handleDrag} style={{
                        "left": `${v[0] * this.state.width}px`,
                        "top": `${v[1] * this.state.height}px`
                    }} onClick={this.handleVertexClick.bind(this, v)} onMouseOver={this.handleVertexMouseOver.bind(this, v)}
                    onMouseOut={this.handleVertexMouseOut.bind(this, v)} onMouseDown={this.handleVertexMouseDown.bind(this, v)}
                    onMouseUp={this.handleVertexMouseUp.bind(this, v)} onMouseMove={this.handleVertexMouseMove.bind(this, v)} />
                ))}
                {this.props.edges.map((e, i) => (
                    <div key={i} className={`edge ${e[2]}`} onDrag={this.handleDrag} style={{
                        "left": `${e[0][0] * this.state.width}px`,
                        "top": `${e[0][1] * this.state.height}px`,
                        "transform": `rotate(${Math.atan2((e[1][1] - e[0][1]) * this.state.height, (e[1][0] - e[0][0]) * this.state.width) - Math.PI / 2}rad)`,
                        "height": `${Math.sqrt(Math.pow((e[0][0] - e[1][0]) * this.state.width, 2) + Math.pow((e[0][1] - e[1][1]) * this.state.height, 2))}px`
                    }} onClick={this.handleEdgeClick.bind(this, e)} onMouseOver={this.handleEdgeMouseOver.bind(this, e)}
                    onMouseOut={this.handleEdgeMouseOut.bind(this, e)} onMouseDown={this.handleEdgeMouseDown.bind(this, e)}
                    onMouseUp={this.handleEdgeMouseUp.bind(this, e)} onMouseMove={this.handleEdgeMouseMove.bind(this, e)} />
                ))}
            </div>
        );
    }
}
