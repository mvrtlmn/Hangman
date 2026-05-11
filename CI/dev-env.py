#!/usr/bin/env python3
# -*- coding: UTF-8 -*-
__version__ = "1.0.1"

# ==============================================================
# module importing
# ==============================================================
from pathlib import Path
from urllib.request import urlretrieve
from zipfile import ZipFile
import logging
import sys

# ==============================================================
# logging
# ==============================================================
LOGGER_FORMAT = "%(asctime)s [%(levelname)s] %(message)s"
LOGGER_DATE_FORMAT = "%Y-%m-%d %H:%M:%S"

logging.basicConfig(
  level=logging.INFO,
  format=LOGGER_FORMAT,
  datefmt=LOGGER_DATE_FORMAT,
  stream=sys.stdout
)

log = logging.getLogger(Path(__file__).name)

# ==============================================================
# paths
# ==============================================================
SCRIPT_DIR = Path(__file__).resolve().parent
REPOSITORY_ROOT = SCRIPT_DIR.parent

LIB_DIR = REPOSITORY_ROOT / "lib"
PACKAGE_DIR = LIB_DIR / "packages"

REFERENCE_ROOT = LIB_DIR / "reference-assemblies"
REFERENCE_ASSEMBLY_DIR = REFERENCE_ROOT / ".NETFramework" / "v4.6.1"

# ==============================================================
# package information
# ==============================================================
PACKAGE_ID = "Microsoft.NETFramework.ReferenceAssemblies.net461"
PACKAGE_VERSION = "1.0.3"

PACKAGE_FILE = PACKAGE_DIR / f"{PACKAGE_ID}.{PACKAGE_VERSION}.nupkg"
PACKAGE_URL = f"https://www.nuget.org/api/v2/package/{PACKAGE_ID}/{PACKAGE_VERSION}"

PACKAGE_REFERENCE_PATH = "build/.NETFramework/v4.6.1/"

# ==============================================================
# version information
# ==============================================================
def show_version_information() -> None:
  log.info(f"dev-env.py {__version__}")
  log.info(f"{PACKAGE_ID} {PACKAGE_VERSION}")

# ==============================================================
# prepare directories
# ==============================================================
def prepare_directories() -> None:
  log.info("Prepare directories")
  PACKAGE_DIR.mkdir(parents=True, exist_ok=True)
  REFERENCE_ASSEMBLY_DIR.mkdir(parents=True, exist_ok=True)

# ==============================================================
# download package
# ==============================================================
def download_package() -> None:
  if not PACKAGE_FILE.exists():
    log.info(f"Download {PACKAGE_ID} {PACKAGE_VERSION}")
    urlretrieve(PACKAGE_URL, PACKAGE_FILE)
  else:
    log.info(f"Package already exists: {PACKAGE_FILE}")

# ==============================================================
# extract reference assemblies
# ==============================================================
def extract_reference_assemblies() -> None:
  log.info("Extract .NET Framework 4.6.1 reference assemblies")

  with ZipFile(PACKAGE_FILE, "r") as package:
    for file_name in package.namelist():
      normalized_file_name = file_name.replace("\\", "/")

      if normalized_file_name.startswith(PACKAGE_REFERENCE_PATH) and not normalized_file_name.endswith("/"):
        relative_file_name = normalized_file_name.replace(PACKAGE_REFERENCE_PATH, "")
        target_file = REFERENCE_ASSEMBLY_DIR / relative_file_name

        target_file.parent.mkdir(parents=True, exist_ok=True)
        target_file.write_bytes(package.read(file_name))

# ==============================================================
# main
# ==============================================================
def main() -> None:
  if "--version" in sys.argv:
    show_version_information()
    return

  log.info("Start preparing development environment")
  log.info(f"Repository root: {REPOSITORY_ROOT}")

  prepare_directories()
  download_package()
  extract_reference_assemblies()

  log.info("Development environment prepared")
  log.info(f"Reference assemblies: {REFERENCE_ASSEMBLY_DIR}")

# ==============================================================
# script entry point
# ==============================================================
if __name__ == "__main__":
  main()