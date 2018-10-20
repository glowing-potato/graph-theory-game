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
    }

    handleRef(el) {
        this.view = el;
        this.handleResize();
    }

    render() {
        return (
            <div className="graph-view" ref={this.handleRef}>
                {this.props.verts.map((v, i) => (
                    <div key={i} className="vert" style={{
                        "left": `${v[0] * this.state.width}px`,
                        "top": `${v[1] * this.state.height}px`
                    }} />
                ))}
                {this.props.edges.map((e, i) => (
                    <div key={i} className="edge" style={{
                        "left": `${e[0][0] * this.state.width}px`,
                        "top": `${e[0][1] * this.state.height}px`,
                        "transform": `rotate(${Math.atan2((e[1][1] - e[0][1]) * this.state.height, (e[1][0] - e[0][0]) * this.state.width) - Math.PI / 2}rad)`,
                        "height": `${Math.sqrt(Math.pow((e[0][0] - e[1][0]) * this.state.width, 2) + Math.pow((e[0][1] - e[1][1]) * this.state.height, 2))}px`
                    }} />
                ))}
            </div>
        );
    }
}
