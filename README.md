# Blackbird.io WordsOnline

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

**WordsOnline** offers AI-powered platform, using Neural Machine Translation technology with human expertise to provide our enterprise customers with cost-effective, fast turnaround language services at optimal quality.

## Before setting up

Before you can connect you need to make sure that:

- You have a WordsOnline instance 
- You have a WordsOnline `API key`
- You have a WordsOnline `Project GUID`

Both the `API key` and the `Project GUID` you can get by asking WordsOnline support.

## Connecting

1. Navigate to Apps, and identify the **WordsOnline** app. You can use search to find it.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My WordsOnline connection'.
4. Fill in the `API key` and the `Project GUID` you got from WordsOnline.
5. Click **Connect**.
6. Make sure that connection was added successfully.

![connection](/image/README/connection.png)

## Actions

### Requests 
- **Search requests**: Searches for requests based on the provided filters
- **Get request**: Get request details based on the provided request ID
- **Update request status**: Updates the status of a request based on the provided ID
- **Create request**: Create a new translation request based on the provided parameters. Auto approving the request is optional, but by default it is set to `true`. Under the hood we receive files, then we zip them and send them to WordsOnline.
- **Download files**: Downloads the files from the request. By default, it will download all files (in all zip files). But you can specify zip which contains the files you want to download.

### Quotes
- **Get quote**: Gets a quote based on the provided request ID
- **Approve quote**: Approves a quote based on the provided request ID

## Events

- **On requests delivered** Triggered when requests are delivered

Note that this is a polling event, so you may not see results immediately. It will depend on the interval you set, which can range from 5 minutes to 7 days.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
