const { build } = require('vite');
const path = require('path');

async function run() {
  try {
    await build({
      root: path.resolve(__dirname, '../PastryFlow/pastryflow-web'),
      configFile: path.resolve(__dirname, '../PastryFlow/pastryflow-web/vite.config.ts'),
    });
    console.log('Build successful');
  } catch (err) {
    console.error('Build failed:');
    console.error(err);
    if (err.errors) {
      console.error('Errors:', JSON.stringify(err.errors, null, 2));
    }
    process.exit(1);
  }
}

run();
