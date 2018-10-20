import React from "react";

export default class LevelMenu extends React.Component {
    render() {
        return (
            <div>
                Level Menu for {this.props.match.params.type}
            </div>
        );
    }
}
