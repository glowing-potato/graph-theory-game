import React from "react";
import { Link } from "react-router-dom";
import background from "./background.svg";
import "./LevelMenu.css";

const levels = [];
for (let i = 1; i <= 50; ++i) {
    levels.push(i);
}

export default class LevelMenu extends React.Component {
    render() {
        return (
            <div className="level-menu">
                <h1>Select Level</h1>
                <main>
                    <div className="padding" />
                    <ul>
                        {levels.map((level, i) => (
                            <li key={i}>
                                <Link to={`/${this.props.match.params.type}/${level}`}>{level}</Link>
                            </li>
                        ))}
                    </ul>
                    <div className="padding" />
                </main>
                <Link to="/">Back to Main Menu</Link>
                <img src={background} alt="" className="background" />
            </div>
        );
    }
}
