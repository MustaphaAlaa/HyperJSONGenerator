# HyperJSONGenerator — High-Performance JSON Data Generator

HyperJSONGenerator is a **high-performance .NET data generator designed to create massive JSON datasets (millions of objects)** with very low memory usage.
It uses **UTF-8 streaming, zero-allocation patterns, and struct-based records** to avoid CPU and RAM spikes, even under extreme output sizes.

Originally built as the data feeder for the [Metriflow analytics pipeline project](https://github.com/MustaphaAlaa/Metriflow), HyperJSONGenerator can also be used as a standalone synthetic-data generator for any large-scale analytics/data-engineering project.

## 🚀 Features

- Generates tens of millions of records without exhausting RAM.
- Uses .NET 9, **Utf8JsonWriter, and streaming write** for maximum performance.
- Struct-based analytics records implementing a shared interface.
- Fully asynchronous file writing.
- Built-in support for:
  - Google Analytics–style data.
  - PageSpeed Insight–style data.
- Runs in Docker and Docker Compose.
- Easy to extend: add your own analytic record types.

## 📂 Project Structure

HyperJSONGenerator:

```HyperJSONGenerator
│
├─ Service/
│   └─ JsonsGenerator.cs
│
├─ Models/
│   ├─ GoogleAnalytics.cs
│   ├─ PageSpeedInsight.cs
│   └─ IAnalyticRecord.cs
│
├─ Json-Files/
│
├─ Program.cs
├─ Dockerfile
├─ docker-compose.yml
└─ README.md
```

## ⚙️ How It Works

### `JsonsGenerator.Generate<T>()`

For each analytic record type:

- Loops through 20 years × 365 days × 24 hours × 23 pages.

  > **Note:** this will be the number of objects per json file, I made it 20 years by default, you can change it in the **Program.cs file**, I tested on 265 years, and produced 53 millions record in seconds, the speed is based on your drive speed, whether it is SSD, or HDD.

- Generates a new record using SetRandoms()

- Writes the object using Utf8JsonWriter

- Flushes the stream every 2 MB

- This ensures:

  - Minimal memory footprint.

  - Fast write speed.

  - Huge file support.

## 🧩 Supported Record Types

- > I changed Types from class to struct to make it easier for the CPU to cache the struct and gain much more speed.
- > **Long data type** is used because I made the time in ticks to avoiding millions of new DateTime structs, this will be much faster and can be easily cached by CPU.
- > I changed Page data type from string to enum: byte, to avoid millions of allocations, and and reduce the pressure on GC, and to gain the benfit from caching, and when the application need the page it's can cast it easily.
- > In [Metriflow analytics pipeline project](https://github.com/MustaphaAlaa/Metriflow) the pages have a table with same IDs that stored in the enum, so the optimization is increased a lot.
- **Each implements IAnalyticRecord:**

  - long Date.

  - byte Page

- **GoogleAnalytics:**

  - Users.

  - Views.

  - Sessions.

- **PageSpeedInsight:**

  - PerformanceScore.

  - LCP_MS.

## Running the Generator

- Running With Docker

  - install docker & docker compose
  - run: `docker compose up --build`

- without docker
  - > **.NET 9 is required.**
- Build: `dotnet build`
- run: `dotnet run`
