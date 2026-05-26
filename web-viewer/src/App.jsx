import { useState } from 'react'

import SoftwareViewer from './components/SoftwareViewer';
import SimulatorDevice from './components/SimulatorDevice';

function App() {


  return (
    
    <div className="grid grid-cols-[2fr_3fr] grid-rows-[auto_1fr] w-screen h-screen bg-[#121212] overflow-hidden text-white font-sans"> 
      {/* Main Header */}
      <div className="col-span-2 bg-[#252526] px-6 py-3 font-mono text-sm border-b border-[#2d2d2d] flex items-center justify-between text-gray-400">
        <div className="flex items-center gap-2">
          <span className="w-2.5 h-2.5 rounded-full bg-orange-500 block"></span>
          <span>ATtiny1626 Simon Says Simulation Dashboard</span>
        </div>
      </div>

      {/* Simulation Section */}
      <div className="bg-[#181818] p-8 h-full flex flex-col justify-center items-center overflow-y-auto">
        <SimulatorDevice />
      </div>

      {/* Code Viewer */}
      <div className="bg-[#1e1e1e] h-full overflow-hidden border-l border-[#2d2d2d]">
        <SoftwareViewer />
      </div>
    </div>
  );
}

export default App
