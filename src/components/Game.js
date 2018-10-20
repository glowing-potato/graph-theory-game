import React from "react";

export default class Game extends React.Component {
    render() {
        return (
            <div className="game-view">
                {this.props.children}
            </div>
        );
    }
}
