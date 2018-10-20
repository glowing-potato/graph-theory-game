import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import MainMenu from "./menus/MainMenu";
import LevelMenu from "./menus/LevelMenu";
import EulerWalk from "./games/EulerWalk";
import HamiltonWalk from "./games/HamiltonWalk";

export default class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <Route path="/euler-walk/:level" component={EulerWalk} />
                    <Route path="/hamilton-walk/:level" component={HamiltonWalk} />
                    <Route path="/:type" component={LevelMenu} />
                    <Route path="/" component={MainMenu} />
                </Switch>
            </BrowserRouter>
        );
    }
}
