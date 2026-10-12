const { spawn } = require('child_process');

const cliPath = require.resolve('@angular/cli/bin/ng');
const nodeMajorVersion = Number(process.versions.node.split('.')[0]);
const nodeOptions = nodeMajorVersion >= 17 ? ['--openssl-legacy-provider'] : [];

const child = spawn(
  process.execPath,
  [...nodeOptions, cliPath, 'serve', ...process.argv.slice(2)],
  { stdio: 'inherit' }
);

child.on('error', (error) => {
  console.error('Failed to start the Angular development server:', error);
  process.exitCode = 1;
});

child.on('exit', (code) => {
  process.exitCode = code === null ? 1 : code;
});
