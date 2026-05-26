import React, { useState } from 'react';
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter';
import { vscDarkPlus } from 'react-syntax-highlighter/dist/esm/styles/prism';

const softwareFiles = import.meta.glob(['@software/**/*.{cs,csproj}', '!@software/**/obj/**'], {
  query: '?raw',
  eager: true,
});

const buildFileTree = (paths) => {
  const root = {};
  paths.forEach(fullpath => {
    const parts = fullpath.replace('../software/', '').split('/');
    let current = root;
    parts.forEach((part, index) => {
      if (!current[part]) {
        current[part] = index === parts.length - 1 ? { __file: fullpath } : {};
      }
      current = current[part];
    });
  });
  return root;
}


const TreeNode = ({ name, node, selectedPath, onSelect }) => {
  const isFile = !!node.__file;
  const [isOpen, setIsOpen] = useState(false);
  if (isFile) {
    const isSelected = node.__file === selectedPath;
    return (
      <div
        onClick={() => onSelect(node.__file)}
        className={`cursor-pointer py-1 pl-6 pr-2 text-xs font-mono flex items-center select-none tracking-wide transition-colors ${isSelected ? 'bg-[#37373d] text-orange-400 font-semibold' : 'hover:bg-[#2a2d2e] text-gray-300'
          }`}
      >
        <span className="mr-1.5">{name.endsWith('.csproj') ? '⚙️' : '📄'}</span>
        {name}
      </div>
    );
  }

  // Node represents an active directory container folder
  return (
    <div className="font-mono text-xs">
      <div
        onClick={() => setIsOpen(!isOpen)}
        className="cursor-pointer py-1 pl-2 pr-2 text-gray-400 hover:bg-[#2a2d2e] flex items-center select-none font-medium tracking-wide"
      >
        <span className={`inline-block mr-1 text-[10px] transform transition-transform duration-100 ${isOpen ? 'rotate-90' : ''}`}>
          ▶
        </span>
        <span className="mr-1.5">📁</span>
        {name}
      </div>
      {isOpen && (
        <div className="border-l border-[#333] ml-3.5 left-panel-shift">
          {Object.keys(node).map((key) => (
            <TreeNode
              key={key}
              name={key}
              node={node[key]}
              selectedPath={selectedPath}
              onSelect={onSelect}
            />
          ))}
        </div>
      )}
    </div>
  );
};

export default function SoftwareViewer() {
  const filePaths = Object.keys(softwareFiles);
  const treeData = buildFileTree(filePaths);

  // Default selection set to main.cpp or fallback to the first file entry
  const defaultFile = filePaths.find(p => p.includes('Program.cs')) || filePaths[0] || '';
  const [selectedPath, setSelectedPath] = useState(defaultFile);

  const getLanguage = (path) => (path.endsWith('.csproj') ? 'xml' : 'csharp');

  return (
    <div className="w-full h-full flex flex-row bg-[#1e1e1e] overflow-hidden select-none">

      {/* VS CODE LEFT SIDEBAR PANEL: Explorer Module */}
      <div className="w-56 h-full bg-[#252526] border-r border-[#2d2d2d] flex flex-col overflow-y-auto">
        <div className="px-4 py-2.5 text-[11px] uppercase tracking-wider text-gray-400 font-bold border-b border-[#2d2d2d] bg-[#252526] select-none">
          Explorer: Workspace
        </div>
        <div className="py-2 flex-1">
          {Object.keys(treeData).map((key) => (
            <TreeNode
              key={key}
              name={key}
              node={treeData[key]}
              selectedPath={selectedPath}
              onSelect={setSelectedPath}
            />
          ))}
        </div>
      </div>

      {/* VS CODE RUNTIME EDITOR: Code Display Sheet */}
      <div className="flex-1 h-full flex flex-col overflow-hidden">
        {/* Active Tab Indicator Header */}
        <div className="bg-[#2d2d2d] h-9 px-4 flex items-center border-b border-[#2d2d2d] text-xs text-orange-400 font-mono tracking-wide font-medium bg-gradient-to-b from-[#2d2d2d] to-[#252526]">
          {selectedPath ? selectedPath.split('/').pop() : 'No active selection'}
        </div>

        {/* Glowing syntax parsing container */}
        <div className="flex-1 overflow-y-auto bg-[#1e1e1e] text-sm relative">
          {selectedPath && softwareFiles[selectedPath] ? (
            <SyntaxHighlighter
              language={getLanguage(selectedPath)}
              style={vscDarkPlus}
              showLineNumbers
              customStyle={{ margin: 0, padding: '1.25rem', background: '#1e1e1e' }}
            >
              {softwareFiles[selectedPath].default}
            </SyntaxHighlighter>
          ) : (
            <div className="p-8 text-gray-500 italic text-center font-mono text-xs">
              Select an embedded code script from the explorer sidebar to begin analysis.
            </div>
          )}
        </div>
      </div>

    </div>
  );
}