// automatically generated from `lib/stdlib.fc` `Manager/manager_code.fc` 
PROGRAM{
  DECLPROC send_message
  DECLPROC loadOwner
  DECLPROC loadData
  DECLPROC saveData
  DECLPROC addContract
  DECLPROC removeContract
  DECLPROC recv_internal
  DECLPROC recv_external
  87552 DECLMETHOD getContract
  100099 DECLMETHOD getSeqno
  102025 DECLMETHOD getOwner
  107201 DECLMETHOD getGtams
  send_message PROC:<{
    0 PUSHINT
    24 PUSHINT
    NEWC
    6 STU
    s0 s6 XCHG2
    STSLICER
    ROT
    STGRAMS
    s1 s4 XCHG
    107 STU
    s1 s2 XCHG
    32 STU
    64 STU
    ENDC
    SWAP
    SENDRAWMSG
  }>
  loadOwner PROCREF:<{
    256 LDU
    8 LDU
    4 LDU
    4 LDU
    LDREF
    DROP
    CTOS
  }>
  loadData PROCREF:<{
    c4 PUSH
    CTOS
    64 LDU
    LDREF
    LDDICT
    DROP
  }>
  saveData PROC:<{
    NEWC
    s1 s3 XCHG
    64 STU
    STREF
    STDICT
    ENDC
    c4 POP
  }>
  addContract PROC:<{
    0 PUSHINT
    SWAP
    256 LDU
    s1 s3 PUSH2
    8 PUSHPOW2
    DICTUGET
    NULLSWAPIFNOT
    NIP
    IFNOT:<{
      s2 POP
      ACCEPT
      SWAP
      LDREF
      DROP
      NEWC
      STREF
      s0 s2 XCHG
      8 PUSHPOW2
      DICTUSETB
      1 PUSHINT
    }>ELSE<{
      2DROP
    }>
    DUP
    38 THROWIFNOT
  }>
  removeContract PROC:<{
    256 LDU
    DROP
    s0 s1 PUSH2
    8 PUSHPOW2
    DICTUGET
    NULLSWAPIFNOT
    NIP
    39 THROWIFNOT
    ACCEPT
    SWAP
    8 PUSHPOW2
    DICTUDELGET
    NULLSWAPIFNOT
    2DROP
    1 PUSHINT
  }>
  recv_internal PROC:<{
    3 BLKDROP
  }>
  recv_external PROC:<{
    9 PUSHPOW2
    LDSLICEX
    LDREF
    DROP
    DUP
    CTOS
    loadData INLINECALLDICT
    s0 s3 XCHG
    64 LDU
    s1 s3 PUXC
    LEQ
    26 THROWIF
    s0 s4 XCHG
    HASHCU
    OVER
    CTOS
    256 PLDU
    s1 s6 s0 XCHG3
    CHKSIGNU
    34 THROWIFNOT
    0 PUSHINT
    SWAP
    32 LDU
    OVER
    4098 PUSHINT
    EQUAL
    IF:<{
      NIP
      DUP
      SREFS
      IF:<{
        NIP
        ACCEPT
        1 PUSHINT
        SWAP
        8 LDU
        LDREF
        DROP
        SWAP
        SENDRAWMSG
      }>ELSE<{
        DROP
      }>
    }>ELSE<{
      OVER
      8193 PUSHINT
      EQUAL
      IF:<{
        NIP
        NIP
        LDREF
        DROP
        CTOS
        addContract CALLDICT
      }>ELSE<{
        OVER
        8194 PUSHINT
        EQUAL
        IF:<{
          NIP
          NIP
          LDREF
          DROP
          CTOS
          removeContract CALLDICT
        }>ELSE<{
          OVER
          4099 PUSHINT
          EQUAL
          IF:<{
            NIP
            LDREF
            DROP
            SETCODE
          }>ELSE<{
            OVER
            4100 PUSHINT
            EQUAL
            IF:<{
              NIP
              s4 POP
              s0 s3 XCHG
              LDREF
              DROP
            }>ELSE<{
              DROP
              4097 PUSHINT
              EQUAL
              IFNOT:<{
                32 THROW
              }>
              s0 s3 XCHG
            }>
            s0 s3 XCHG
          }>
        }>
      }>
    }>
    0 EQINT
    IF:<{
      ACCEPT
    }>
    s1 s2 XCHG
    saveData CALLDICT
  }>
  getContract PROC:<{
    loadData INLINECALLDICT
    NIP
    NIP
    8 PUSHPOW2
    DICTUGETNEXT
    NULLSWAPIFNOT
    NULLSWAPIFNOT
    IFJMP:<{
      SWAP
      LDREF
      DROP
      CTOS
      264 PUSHINT
      LDSLICEX
      LDREF
      DROP
      CTOS
    }>
    2DROP
    NEWC
    ENDC
    CTOS
    -1 PUSHINT
    s1 s0 XCPU
  }>
  getSeqno PROC:<{
    c4 PUSH
    CTOS
    32 PLDU
  }>
  getOwner PROC:<{
    loadData INLINECALLDICT
    DROP
    NIP
    CTOS
    loadOwner INLINECALLDICT
  }>
  getGtams PROC:<{
    BALANCE
    UNPAIR
    DROP
  }>
}END>c
