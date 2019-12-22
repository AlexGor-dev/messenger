// automatically generated from `../lib/stdlib.fc` `messenger_code.fc` 
PROGRAM{
  DECLPROC send_message
  DECLPROC loadOwner
  DECLPROC loadMembers
  DECLPROC saveMembers
  DECLPROC loadMessages
  DECLPROC saveMessages
  DECLPROC loadData
  DECLPROC loadMinData
  DECLPROC saveData
  DECLPROC sendMessageBack
  DECLPROC addMember
  DECLPROC removeMember
  DECLPROC addMessage
  DECLPROC removeMessage
  DECLPROC recv_internal
  DECLPROC recv_external
  125090 DECLMETHOD getMember
  123094 DECLMETHOD getMessages
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
    CTOS
    256 LDU
    8 LDU
    4 LDU
    4 LDU
    LDREF
    DROP
    CTOS
  }>
  loadMembers PROCREF:<{
    CTOS
    LDDICT
    LDDICT
    DROP
  }>
  saveMembers PROC:<{
    NEWC
    s1 s2 XCHG
    STDICT
    STDICT
    ENDC
  }>
  loadMessages PROCREF:<{
    CTOS
    64 LDU
    LDDICT
    DROP
  }>
  saveMessages PROC:<{
    NEWC
    s1 s2 XCHG
    64 STU
    STDICT
    ENDC
  }>
  loadData PROCREF:<{
    c4 PUSH
    CTOS
    64 LDU
    LDREF
    LDREF
    LDREF
    DROP
  }>
  loadMinData PROCREF:<{
    c4 PUSH
    CTOS
    64 LDU
    LDREF
    DROP
  }>
  saveData PROC:<{
    NEWC
    s1 s4 XCHG
    64 STU
    s1 s2 XCHG
    STREF
    STREF
    STREF
    ENDC
    c4 POP
  }>
  sendMessageBack PROC:<{
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
    32 STU
    ENDC
    SWAP
    SENDRAWMSG
  }>
  addMember PROC:<{
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
  removeMember PROC:<{
    0 PUSHINT
    s0 s2 XCHG
    8 PUSHPOW2
    DICTUDELGET
    NULLSWAPIFNOT
    NIP
    IF:<{
      NIP
      ACCEPT
      1 PUSHINT
    }>ELSE<{
      SWAP
    }>
    DUP
    38 THROWIFNOT
  }>
  addMessage PROC:<{
    loadData INLINECALLDICT
    s2 PUSH
    loadOwner INLINECALLDICT
    s2 s4 XCHG
    4 BLKDROP
    s2 PUSH
    loadMembers INLINECALLDICT
    s0 s7 XCHG
    256 LDU
    s1 s8 PUSH2
    8 PUSHPOW2
    DICTUGET
    NULLSWAPIFNOT
    NIP
    41 THROWIF
    s1 s2 PUSH2
    8 PUSHPOW2
    DICTUGET
    NULLSWAPIFNOT
    NIP
    IFNOT:<{
      s5 POP
      s0 s2 XCHG
      1 EQINT
      35 THROWIF
      s8 PUSH
      REWRITESTDADDR
      NEWC
      s1 s2 XCHG
      8 STU
      256 STU
      ENDC
      NEWC
      STREF
      s2 s1 PUXC
      8 PUSHPOW2
      DICTUSETB
      s0 s6 XCHG2
      saveMembers CALLDICT
    }>ELSE<{
      s8 s5 XCHG2
      s0 s3 XCHG
      3 BLKDROP
    }>
    SWAP
    loadMessages INLINECALLDICT
    SWAP
    INC
    s0 s3 XCHG
    LDREF
    DROP
    s7 PUSH
    NEWC
    32 STU
    s1 s7 XCHG
    256 STU
    s1 s6 XCHG
    STREF
    SWAP
    64 PUSHINT
    s3 s6 s6 PUXC2
    DICTUSETB
    saveMessages CALLDICT
    s1 s3 s0 XCHG3
    saveData CALLDICT
    32 PUSHPOW2DEC
    0 PUSHINT
    s1 s2 XCHG
    64 PUSHINT
    sendMessageBack CALLDICT
  }>
  removeMessage PROC:<{
    0 PUSHINT
    s0 s3 XCHG
    loadMessages INLINECALLDICT
    NIP
    OVER
    0 EQINT
    IF:<{
      NIP
      UNTIL:<{
        DUP
        64 PUSHINT
        DICTUREMMIN
        NULLSWAPIFNOT
        NULLSWAPIFNOT
        s2 POP
        s1 s0 XCPU
        IF:<{
          s1 s4 XCPU
          LEQ
          IF:<{
            s2 POP
            s3 PUSH
            0 EQINT
            IF:<{
              ACCEPT
            }>
            s0 s3 XCHG
            INC
          }>ELSE<{
            s0 s4 s4 XCHG3
            DROP
          }>
        }>ELSE<{
          s3 s5 XCHG
          s3 s1 s3 XCHG3
          2DROP
        }>
        SWAP
        NOT
        s1 s3 XCHG
      }>
    }>ELSE<{
      OVER
      1 EQINT
      IF:<{
        NIP
        s1 s(-1) PUXC
        64 PUSHINT
        DICTUDELGET
        NULLSWAPIFNOT
        NIP
        IF:<{
          s2 POP
          ACCEPT
          1 PUSHINT
        }>ELSE<{
          s0 s2 XCHG
        }>
      }>ELSE<{
        SWAP
        2 EQINT
        IF:<{
          UNTIL:<{
            -1 PUSHINT
            OVER
            64 PUSHINT
            DICTUGETNEXT
            NULLSWAPIFNOT
            NULLSWAPIFNOT
            s2 POP
            OVER
            IF:<{
              ROT
              64 PUSHINT
              DICTUDELGET
              NULLSWAPIFNOT
              2DROP
              s3 PUSH
              0 EQINT
              IF:<{
                ACCEPT
              }>
              s0 s3 XCHG
              INC
            }>ELSE<{
              s1 s4 s0 XCHG3
              DROP
            }>
            SWAP
            NOT
            s1 s3 XCHG
          }>
        }>
        s0 s2 XCHG
      }>
      s0 s2 XCHG
    }>
    s2 PUSH
    38 THROWIFNOT
    saveMessages CALLDICT
    SWAP
  }>
  recv_internal PROC:<{
    s2 POP
    CTOS
    4 LDU
    SWAP
    1 PUSHINT
    AND
    IFJMP:<{
      2DROP
    }>
    OVER
    SEMPTY
    IFJMP:<{
      2DROP
    }>
    LDMSGADDR
    DROP
    SWAP
    32 LDU
    OVER
    0 EQINT
    IFJMP:<{
      3 BLKDROP
    }>
    32 LDU
    s0 s2 XCHG
    8194 PUSHINT
    EQUAL
    IFJMP:<{
      SWAP
      addMessage CALLDICT
    }>
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
    s0 s4 XCHG
    64 LDU
    s1 s4 PUXC
    LEQ
    26 THROWIF
    s0 s5 XCHG
    HASHCU
    s2 PUSH
    CTOS
    256 PLDU
    s1 s7 s0 XCHG3
    CHKSIGNU
    34 THROWIFNOT
    0 PUSHINT
    s0 s2 XCHG
    32 LDU
    OVER
    4098 PUSHINT
    EQUAL
    IF:<{
      NIP
      DUP
      SREFS
      IF:<{
        s2 POP
        ACCEPT
        1 PUSHINT
        s0 s2 XCHG
        8 LDU
        LDREF
        DROP
        SWAP
        SENDRAWMSG
      }>ELSE<{
        DROP
      }>
      OVER
      44 THROWIFNOT
    }>ELSE<{
      OVER
      8195 PUSHINT
      EQUAL
      IF:<{
        NIP
        s2 POP
        SWAP
        64 LDU
        2 LDU
        DROP
        s2 s3 XCHG
        removeMessage CALLDICT
      }>ELSE<{
        OVER
        8193 PUSHINT
        EQUAL
        IF:<{
          NIP
          s2 POP
          s0 s4 XCHG
          loadMembers INLINECALLDICT
          s0 s2 XCHG
          LDREF
          DROP
          CTOS
          addMember CALLDICT
          s0 s2 XCHG
          saveMembers CALLDICT
        }>ELSE<{
          OVER
          8196 PUSHINT
          EQUAL
          IF:<{
            NIP
            s2 POP
            s0 s4 XCHG
            loadMembers INLINECALLDICT
            s0 s2 XCHG
            256 LDU
            DROP
            removeMember CALLDICT
            s0 s2 XCHG
            saveMembers CALLDICT
          }>ELSE<{
            OVER
            8197 PUSHINT
            EQUAL
            IF:<{
              NIP
              s2 POP
              s0 s4 XCHG
              loadMembers INLINECALLDICT
              s0 s2 XCHG
              LDREF
              DROP
              CTOS
              s1 s2 XCHG
              addMember CALLDICT
              -ROT
              saveMembers CALLDICT
            }>ELSE<{
              OVER
              8198 PUSHINT
              EQUAL
              IF:<{
                NIP
                s2 POP
                s0 s4 XCHG
                loadMembers INLINECALLDICT
                s0 s2 XCHG
                256 LDU
                DROP
                s1 s2 XCHG
                removeMember CALLDICT
                -ROT
                saveMembers CALLDICT
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
                    NIP
                    LDREF
                    DROP
                  }>ELSE<{
                    DROP
                    4097 PUSHINT
                    EQUAL
                    IFNOT:<{
                      32 THROW
                    }>
                  }>
                }>
                s0 s4 XCHG
              }>
            }>
          }>
        }>
        s4 s4 s4 XCHG3
      }>
      ROT
    }>
    SWAP
    0 EQINT
    IF:<{
      ACCEPT
    }>
    s0 s3 s3 XCHG3
    saveData CALLDICT
  }>
  getMember PROC:<{
    loadData INLINECALLDICT
    s1 s3 XCHG
    3 BLKDROP
    loadMembers INLINECALLDICT
    DROP
    8 PUSHPOW2
    DICTUGETNEXT
    NULLSWAPIFNOT
    NULLSWAPIFNOT
    IFJMP:<{
      SWAP
      LDREF
      DROP
      CTOS
    }>
    2DROP
    NEWC
    ENDC
    CTOS
    -1 PUSHINT
    SWAP
  }>
  getMessages PROC:<{
    loadData INLINECALLDICT
    s0 s3 XCHG
    3 BLKDROP
    loadMessages INLINECALLDICT
    NIP
    NEWC
    ENDC
    CTOS
    -ROT
    64 PUSHINT
    DICTUGETNEXT
    NULLSWAPIFNOT
    NULLSWAPIFNOT
    IFJMP:<{
      SWAP
      32 LDU
      256 LDU
      LDREF
      DROP
      CTOS
      DUP
      SBITS
      LDSLICEX
      s5 s5 s5 PUSH3
      s8 s8 s3 PUSH3
      SREFS
      IF:<{
        s4 POP
        s0 s4 XCHG
        LDREF
        DROP
        CTOS
        DUP
        SBITS
        LDSLICEX
      }>ELSE<{
        s5 s4 XCHG2
        SWAP
      }>
      DUP
      SREFS
      IF:<{
        s3 POP
        s0 s2 XCHG
        LDREF
        DROP
        CTOS
        DUP
        SBITS
        LDSLICEX
      }>ELSE<{
        s1 s3 XCHG
      }>
      DUP
      SREFS
      IF:<{
        s2 POP
        SWAP
        LDREF
        DROP
        CTOS
        DUP
        SBITS
        LDSLICEX
      }>ELSE<{
        s1 s2 XCHG
      }>
      DUP
      SREFS
      IF:<{
        s5 POP
        s0 s4 XCHG
        LDREF
        DROP
        CTOS
        DUP
        SBITS
        LDSLICEX
      }>ELSE<{
        s1 s5 XCHG
      }>
      DUP
      SREFS
      IF:<{
        s4 POP
        s0 s3 XCHG
        LDREF
        DROP
        CTOS
        DUP
        SBITS
        LDSLICEX
      }>ELSE<{
        s1 s4 XCHG
      }>
      DUP
      SREFS
      IF:<{
        s10 POP
        s0 s9 XCHG
        LDREF
        DROP
        CTOS
        DUP
        SBITS
        LDSLICEX
        DROP
      }>ELSE<{
        s1 s10 XCHG
        DROP
      }>
      s8 s9 XCHG
      s7 s8 XCHG
      s6 s7 XCHG
      s5 s6 XCHG
      s5 s4 s0 XCHG3
      s3 s3 s0 XCHG3
    }>
    2DROP
    -1 PUSHINT
    0 PUSHINT
    s0 s1 s2 XCPUXC
    s0 s0 s0 PUSH3
    s0 s0 s0 PUSH3
  }>
  getSeqno PROC:<{
    c4 PUSH
    CTOS
    32 PLDU
  }>
  getOwner PROC:<{
    loadMinData INLINECALLDICT
    NIP
    loadOwner INLINECALLDICT
  }>
  getGtams PROC:<{
    BALANCE
    UNPAIR
    DROP
  }>
}END>c
