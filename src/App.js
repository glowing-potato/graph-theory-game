import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import MainMenu from "./menus/MainMenu";
import LevelMenu from "./menus/LevelMenu";
import AboutMenu from "./menus/AboutMenu";
import LevelsMenu from "./menus/LevelsMenu";
import SettingsMenu from "./menus/SettingsMenu";
import EulerTrail from "./games/EulerTrail";
import HamiltonPath from "./games/HamiltonPath";
import EulerCycle from "./games/EulerCycle";
import HamiltonCircuit from "./games/HamiltonCircuit";

export default class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <Route path="/euler-trail/:level" component={EulerTrail} />
                    <Route path="/hamilton-path/:level" component={HamiltonPath} />
                    <Route path="/euler-cycle/:level" component={EulerCycle} />
                    <Route path="/hamilton-circuit/:level" component={HamiltonCircuit} />
                    <Route path="/about" component={AboutMenu} />
                    <Route path="/levels" component={LevelsMenu} />
                    <Route path="/settings" component={SettingsMenu} />
                    <Route path="/:type" component={LevelMenu} />
                    <Route path="/" component={MainMenu} />
                </Switch>
            </BrowserRouter>
        );
    }
}
