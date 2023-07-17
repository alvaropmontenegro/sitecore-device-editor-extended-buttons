# Sitecore Device Editor - CopyTo Button

## Description

The "CopyTo" button is a custom extension for the Sitecore Device Editor, allowing users to clone and copy a selected rendering from the current device to a target item's layout on the same or a different device. This functionality simplifies the process of reusing renderings across various items within the Sitecore environment.

## Features

- Copy a selected rendering to a target item's layout.
- Display a modal dialog to select the target item.
- Validate input and handle potential errors, displaying appropriate alert messages.

## Installation

### Option 1
1. Download the package in the Releases page.
2. Install the package in your Sitecore instance as a regular package. It'll install all the necessary files
3. The "CopyTo" button will be available in the Sitecore Device Editor once the installation is finished.

### Option 2
1. Clone or download the repository.
2. Build the project to generate the assembly.
3. Deploy the assembly (`CopyTo.dll`) to your Sitecore instance's `/bin` folder.
4. Copy the /sitecore/ folder to your Sitecore instance's folder.
5. The "CopyTo" button will be available in the Sitecore Device Editor once the assembly is deployed.

## Usage

1. Log in to your Sitecore instance as an administrator.
2. Navigate to the "Device Editor" in the "Layout" tab of the "Presentation" section.
3. Select a rendering that you want to copy from the current device.
4. Click the "CopyTo" button to initiate the copy process.
5. A modal dialog will appear, allowing you to select the target item for the copied rendering.
6. Choose the target item and click "OK" to proceed with the copy operation.
7. If the copy is successful, you will see an alert message confirming the operation. If any errors occur, an appropriate error message will be displayed.

## Dependencies

The "CopyTo" button is built using the Sitecore Experience Platform 10.3, and it relies on the following Sitecore assemblies:

- Sitecore.Kernel.dll
- Sitecore.Client.dll
- Sitecore.Logging.dll

Ensure that these assemblies are available in your Sitecore environment.

## Configuration

There are no additional configuration steps required for the "CopyTo" button. It is designed to work out of the box after installation or deployment.

## Contributing

Contributions to this project are welcome. If you encounter any issues or have suggestions for improvements, please open an issue or create a pull request.

## Authors

- [Álvaro Montenegro](https://github.com/alvaropmontenegro) - Initial implementation.
