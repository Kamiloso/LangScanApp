#!/bin/sh
set -eu
umask 077

REPO="Kamiloso/LangScanApp"
BRANCH="production"
BASE_URL="https://raw.githubusercontent.com/${REPO}/${BRANCH}"

SELF_PATH="backend/Build/update.sh"

FILES="
backend/Build/docker-compose.yml
"

KEEP_FILES="
backend/Build/.env
"

SCRIPT_DIR=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
SCRIPT_PATH="${SCRIPT_DIR}/$(basename -- "$0")"
cd "$SCRIPT_DIR"

self_update() {
    [ -n "$SELF_PATH" ] || return 0
    [ "${UPDATED_SELF:-0}" != "1" ] || return 0

    tmp_file=$(mktemp "${SCRIPT_PATH}.XXXXXX")

    if ! curl -fsSL "${BASE_URL}/${SELF_PATH}" -o "${tmp_file}"; then
        rm -f "${tmp_file}"
        echo "Failed to download ${SELF_PATH}" >&2
        exit 1
    fi

    if ! cmp -s "${tmp_file}" "${SCRIPT_PATH}"; then
        chmod 700 "${tmp_file}"
        mv -f "${tmp_file}" "${SCRIPT_PATH}"
        UPDATED_SELF=1 exec "${SCRIPT_PATH}" "$@"
    fi

    rm -f "${tmp_file}"
}

download_file() {
    src_path="$1"
    dest_path=$(basename -- "$src_path")
    tmp_file=$(mktemp "${dest_path}.XXXXXX")

    if ! curl -fsSL "${BASE_URL}/${src_path}" -o "${tmp_file}"; then
        rm -f "${tmp_file}"
        echo "Failed to download ${src_path}" >&2
        exit 1
    fi

    chmod 600 "${tmp_file}"
    mv -f "${tmp_file}" "${dest_path}"
}

process_list() {
    list="$1"
    overwrite="$2"

    printf '%s\n' "$list" | while IFS= read -r src_path; do
        case "$src_path" in
            (""|\#*) continue ;;
        esac

        dest_path=$(basename -- "$src_path")

        if [ "$overwrite" = "1" ] || [ ! -f "$dest_path" ]; then
            download_file "$src_path"
        fi
    done
}

self_update "$@"
process_list "$FILES" 1
process_list "$KEEP_FILES" 0