import React, { useEffect, useRef, useState } from 'react';

export default function ConsoleSandbox() {
  const containerRef = useRef(null);

  useEffect(() => {
    // 1. Create the official script element
    const script = document.createElement('script');
    script.src = 'https://asciinema.org/a/EBSL3aYyanjvkA6O.js';
    script.id = 'asciicast-EBSL3aYyanjvkA6O';
    script.async = true;

    // 2. Pass layout and speed parameters right to the script
    script.setAttribute('data-speed', '0.6');
    script.setAttribute('data-autoplay', '1');
    script.setAttribute('data-loop', '1');
    script.setAttribute('data-size', 'responsive');
    // script.setAttribute('data-rows', '16');

    // 3. Inject the script into our local reference container
    if (containerRef.current) {
      containerRef.current.appendChild(script);
    }

    // 4. Cleanup function to wipe the script if you navigate away
    return () => {
      if (containerRef.current) {
        containerRef.current.innerHTML = '';
      }
    };
  }, []);

  return (
    <div className="w-full flex flex-col items-center justify-center p-4 bg-[#181818] rounded-xl border border-[#2d2d2d] shadow-inner h-full select-none">
      <span className="text-[10px] font-mono text-gray-500 uppercase tracking-widest mb-4 border border-[#2d2d2d] px-2 py-0.5 rounded">
        Live .NET Runtime Environment
      </span>

      {/* TERMINAL HOUSING WINDOW (Windows cmd / Linux Bash Enclosure Style) */}
      <div className="w-full h-auto bg-[#121314] rounded-lg border border-[#333] shadow-2xl overflow-hidden flex flex-col">

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

        {/* Asciienema Viewer */}
        <div ref={containerRef} className="w-full h-auto flex-col" />
      </div>

      {/* Footnote status ribbon */}
      <div className="mt-4 flex flex-row gap-4 text-[11px] font-mono text-gray-500 border-t border-[#2d2d2d] pt-3 w-full justify-around">
        <span>RUNTIME: <span className="text-orange-400 font-bold">.NET 8.0</span></span>
        <span>STATUS: <span className="text-green-500 font-bold">READY</span></span>
      </div>
    </div>
  );
}
