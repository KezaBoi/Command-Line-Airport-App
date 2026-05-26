import React, { useState } from 'react';

export default function ConsoleSandbox() {
  return (
    <div className="w-full flex flex-col items-center justify-center p-4 bg-[#181818] rounded-xl border border-[#2d2d2d] shadow-inner h-full select-none">
      <span className="text-[10px] font-mono text-gray-500 uppercase tracking-widest mb-4 border border-[#2d2d2d] px-2 py-0.5 rounded">
        Live .NET Runtime Environment
      </span>

      {/* 💻 THE TERMINAL HOUSING WINDOW (Windows cmd / Linux Bash Enclosure Style) */}
      <div className="w-full h-[520px] bg-[#0c0c0c] rounded-lg border border-[#333] shadow-2xl overflow-hidden flex flex-col">
        
        {/* Top Header Control Strip */}
        <div className="bg-[#2d2d2d] px-4 py-2 border-b border-[#333] flex items-center justify-between text-xs text-gray-400 font-mono">
          <div className="flex items-center gap-2">
            <span className="w-2.5 h-2.5 rounded-full bg-orange-500 block"></span>
            <span>cmd.exe — pst2.exe</span>
          </div>
          <div className="flex gap-2 text-gray-500 font-sans text-xs">
            <span>─</span> <span>▢</span> <span>✕</span>
          </div>
        </div>

        {/* 🚀 THE LIVE CODESANDBOX ENGINE IFRAME
            - Swap 'YOUR_BOX_ID_HERE' with your actual unique CodeSandbox share ID string.
            - view=preview: Forces it to load straight into your interactive terminal view.
            - hidenavigation=1: Hides the editor workspace menu bars for a pure layout look.
        */}
        <iframe
          src="https://codesandbox.io/embed/github/KezaBoi/Command-Line-Airport-App/main?codemirror=0&embed=1&hideexplorer=1&hidenavigation=1&initialpath=terminal&sidebar=0&view=terminal"
          className="w-full flex-1 border-none bg-[#0c0c0c]"
          title="C# .NET Airport Application Workspace"
          allow="accelerometer; encrypted-media; gyroscope; hid; microphone; midi; payment; usb"
          sandbox="allow-forms allow-modals allow-popups allow-presentation allow-same-origin allow-scripts"
        />
      </div>
      
      {/* Footnote status ribbon */}
      <div className="mt-4 flex flex-row gap-4 text-[11px] font-mono text-gray-500 border-t border-[#2d2d2d] pt-3 w-full justify-around">
        <span>RUNTIME: <span className="text-orange-400 font-bold">.NET 8.0</span></span>
        <span>STATUS: <span className="text-green-500 font-bold">READY</span></span>
      </div>
    </div>
  );
}


// import React from 'react';
// import { SandpackProvider, SandpackConsole } from "@codesandbox/sandpack-react";

// // 1. Grab your raw handcrafted C# source files exactly like your right-hand tree does!
// const consoleProjectFiles = import.meta.glob([
//   '@software/**/*.{cs,csproj}',
//   '!@software/**/obj/**'
// ], {
//   query: '?raw',
//   eager: true,
// });

// export default function ConsoleSandbox() {
//   // 2. Format the files into the JSON structure Sandpack expects
//   const sandpackFiles = {};
//   Object.keys(consoleProjectFiles).forEach((path) => {
//     const cleanFileName = path.split('/').pop();
//     sandpackFiles[`/${cleanFileName}`] = consoleProjectFiles[path].default;
//   });

//   return (
//     <div className="w-full flex flex-col items-center justify-center p-4 bg-[#181818] rounded-xl border border-[#2d2d2d] shadow-inner h-full select-none">
//       <span className="text-[10px] font-mono text-gray-500 uppercase tracking-widest mb-4 border border-[#2d2d2d] px-2 py-0.5 rounded">
//         Interactive Execution Environment
//       </span>

//       {/* HARDWARE SHELL BOX */}
//       <div className="w-full h-[520px] bg-[#0c0c0c] rounded-lg border border-[#333] shadow-2xl overflow-hidden flex flex-col">
        
//         {/* Windows Command Prompt Header Strip */}
//         <div className="bg-[#2d2d2d] px-4 py-2 border-b border-[#333] flex items-center justify-between text-xs text-gray-400 font-mono">
//           <div className="flex items-center gap-2">
//             <span className="w-2.5 h-2.5 rounded-full bg-orange-500 block"></span>
//             <span>cmd.exe — airport_booking.exe</span>
//           </div>
//           <div className="flex gap-2 text-gray-500 font-sans text-xs">
//             <span>─</span> <span>▢</span> <span>✕</span>
//           </div>
//         </div>

//         {/* NATIVE SANDPACK CONSOLE COMPONENT */}
//         <div className="flex-1 bg-[#0c0c0c] p-2 overflow-hidden text-left font-mono">
//           <SandpackProvider 
//             template="node" 
//             files={sandpackFiles}
//             theme="dark"
//           >
//             <SandpackConsole 
//               showExpandedActions={false}
//               className="h-full border-none font-mono text-xs"
//               customStyle={{
//                 background: '#0c0c0c',
//                 height: '460px',
//                 fontFamily: 'Courier New, monospace'
//               }}
//             />
//           </SandpackProvider>
//         </div>

//       </div>
      
//       <div className="mt-4 flex flex-row gap-4 text-[11px] font-mono text-gray-500 border-t border-[#2d2d2d] pt-3 w-full justify-around">
//         <span>ENGINE: <span className="text-orange-400 font-bold">Sandpack Runtime</span></span>
//         <span>STATUS: <span className="text-green-500 font-bold">ONLINE</span></span>
//       </div>
//     </div>
//   );
// }
